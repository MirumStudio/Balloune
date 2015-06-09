/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using Radix.Event;
using UnityEngine;


//TODO; put logic into WorldMapController
public class WordlMapCharacter : MonoBehaviour {

    [SerializeField]
    private int m_Speed = 2;

    [SerializeField]
    private float m_WaitTime = 0.2f;

    [SerializeField]
    private GameObject m_LevelPointListParent = null;

    private int mCurrentLevel = 0;
    private bool mIsMoving = false;
    private int mFinalDestination = 0;
    private Vector3 mDestination;
	
    private bool mHaveToWait = false;
    private float mTimer = 0;
    
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
        if(mHaveToWait)
        {
            mTimer += Time.deltaTime;
            if (mTimer > m_WaitTime)
            {
                mHaveToWait = false;
                mTimer = 0;
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
            EventService.DispatchEvent(EWorldMapEvent.BEGIN_CHANGE_LEVEL, wantedLevel);
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
                mHaveToWait = true;
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
        EventService.DispatchEvent(EWorldMapEvent.END_CHANGE_LEVEL, mLevelPointList.GetLevelPoint(mCurrentLevel));
    }
}
