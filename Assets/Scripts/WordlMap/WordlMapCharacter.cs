using UnityEngine;
using System.Collections;
using Radix.Event;

public class WordlMapCharacter : MonoBehaviour {

    [SerializeField]
    private int m_Speed = 2;
    public float waittime = 0.5f;
    public int mCurrentLevel = 0;
    private bool mIsMoving = false;
    private int mFinalDestination = 0;
    private Vector3 mDestination;
    private bool wait = false;
    private float timelol = 0;
    public GameObject lol;
    private LevelPointController mLevelPointList;
	void Start () {
        EventListener.Register(EWorldMapEvent.WANT_CHANGE_LEVEL, OnPlayerWantToChangeLevel);
        mLevelPointList = lol.GetComponent<LevelPointController>();
        mCurrentLevel = 0;
	}


	void Update () {
        
        if(wait)
        {
            timelol += Time.deltaTime;
            if(timelol > waittime)
            {
                wait = false;
                timelol = 0;
            }
        }
        else if(mIsMoving)
        {
            ContinueMovingToDestination();
        }
	}

    public void OnPlayerWantToChangeLevel(System.Enum pEvent, object pArg)
    {
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
            mDestination = mLevelPointList.GetLevelPointTransform(mCurrentLevel - 1).position;
            mCurrentLevel--;
        }
        else if(mFinalDestination > mCurrentLevel)
        {
            mDestination = mLevelPointList.GetLevelPointTransform(mCurrentLevel + 1).position;
            mCurrentLevel++;
        }
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
                EventService.DipatchEvent(EWorldMapEvent.END_CHANGE_LEVEL, mLevelPointList.GetLevelPoint(mCurrentLevel));
                //RaiseOnLevelChanged();
            }
        }
    }
}
