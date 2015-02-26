using UnityEngine;
using System.Collections;
using Radix.Event;
using Radix.Error;

public class WordlMapCharacter : MonoBehaviour {

    [SerializeField]
    private int m_Speed = 2;

    [SerializeField]
    private float m_WaitTime = 0.2f;

    [SerializeField]
    private GameObject m_LevelPointListParent;

    private int mCurrentLevel = 0;
    private bool mIsMoving = false;
    private int mFinalDestination = 0;
    private Vector3 mDestination;
    private bool wait = false;
    private float timer = 0;
    
    private LevelPointController mLevelPointList;

    public int CurrentLevel
    {
        get { return mCurrentLevel; }
    }

	void Start () {
        EventListener.Register(EWorldMapEvent.WANT_CHANGE_LEVEL, OnPlayerWantToChangeLevel);
        mLevelPointList = m_LevelPointListParent.GetComponent<LevelPointController>();
        mCurrentLevel = 0; //TODO : load on database
	}


	void Update () 
    {
        if(wait)
        {
            timer += Time.deltaTime;
            if (timer > m_WaitTime)
            {
                wait = false;
                timer = 0;
            }
        }
        else if(mIsMoving)
        {
            ContinueMovingToDestination();
        }
	}

    public void OnPlayerWantToChangeLevel(System.Enum pEvent, object pArg)
    {
        Assert.CheckNull(pArg);
        Assert.Check(pArg is int);

        int wantedLevel = (int)pArg;
        if(wantedLevel != mCurrentLevel && !mIsMoving)
        {
            EventService.DipatchEvent(EWorldMapEvent.BEGIN_CHANGE_LEVEL, wantedLevel);
            mFinalDestination = wantedLevel;
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        if(mFinalDestination < mCurrentLevel)
        {
            mCurrentLevel--;
        }
        else if(mFinalDestination > mCurrentLevel)
        {
            mCurrentLevel++;
        }
        mDestination = mLevelPointList.GetLevelPointTransform(mCurrentLevel).position;
        mIsMoving = true;
    }

    private void ContinueMovingToDestination()
    {
        var step = m_Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mDestination, step);
        if (mDestination.Equals(transform.position))
        {
            if (mCurrentLevel != mFinalDestination)
            {
                wait = true;
                ChangeLevel();
            }
            else
            {
                mIsMoving = false;
                RaiseOnLevelChanged();
            }
        }
    }

    private void RaiseOnLevelChanged()
    {
        EventService.DipatchEvent(EWorldMapEvent.END_CHANGE_LEVEL, mLevelPointList.GetLevelPoint(mCurrentLevel));
    }
}
