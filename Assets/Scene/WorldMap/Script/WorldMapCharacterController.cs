using UnityEngine;
using System.Collections;
using System;

public delegate void EventStringHandler(string pString);
public class WorldMapCharacterController : MonoBehaviour {

    public event EventStringHandler OnLevelChanged;
    public event EventHandler OnLevelExited;

    [SerializeField]
    private int m_Speed = 2;

    [SerializeField]
    private GameObject m_LevelPoint;
    private LevelPointList mLevelPointList;

    private bool mIsMoving = false;
    private Vector3 mDestination;

    private Transform mCurrentLevel;

	void Start () {
        mLevelPointList = m_LevelPoint.GetComponent<LevelPointList>();
        mCurrentLevel = mLevelPointList.GetLevelPointTransform("Level1"); //Need to be loaded from DataBase. (progresssion of player)
        transform.position = AjustPositionWithCharacterSprite(mCurrentLevel.position);
	}
	
	void Update () {
        if (!mIsMoving)
        {
            CheckInput();
        }
        else
        {
            ContinueMovingToDestination();
        }
	}

    public LevelPoint GetCurrentLevelPoint()
    {
        return mCurrentLevel.GetComponent<LevelPoint>();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            CheckLevelId(GetCurrentLevelPoint().UpLevelPoint);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            CheckLevelId(GetCurrentLevelPoint().BottomLevelPoint);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            CheckLevelId(GetCurrentLevelPoint().LeftLevelPoint);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            CheckLevelId(GetCurrentLevelPoint().RightLevelPoint);
        }
    }

    private void CheckLevelId(string pLevelId)
    {
        if(!string.IsNullOrEmpty(pLevelId)) // && UNLOCK
        {
            ChangeLevel(pLevelId);
        }
    }

    private void ChangeLevel(string pLevelId)
    {
        mCurrentLevel = mLevelPointList.GetLevelPointTransform(pLevelId);
        mDestination = mCurrentLevel.position;
        AjustDestination();
        RaiseOnLevelExited();
        mIsMoving = true;
    }

    private void AjustDestination()
    {
        mDestination = AjustPositionWithCharacterSprite(mDestination);
    }

    private Vector3 AjustPositionWithCharacterSprite(Vector3 pPosition)
    {
        Vector3 newPosition = new Vector3();
        newPosition.y = pPosition.y + 0.35f;
        newPosition.x = pPosition.x - 0.05f;
        newPosition.z = pPosition.z;

        return newPosition;
    }

    private void ContinueMovingToDestination()
    {
        var step = m_Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mDestination, step);
        if (mDestination.Equals(transform.position))
        {
            mIsMoving = false;
            RaiseOnLevelChanged();
        }
    }

    private void RaiseOnLevelExited()
    {
        if(OnLevelExited != null)
        {
            OnLevelExited(this, null);
        }
    }

    private void RaiseOnLevelChanged()
    {
        if(OnLevelChanged != null)
        {
            OnLevelChanged(GetCurrentLevelPoint().Id);
        }
    }
}
