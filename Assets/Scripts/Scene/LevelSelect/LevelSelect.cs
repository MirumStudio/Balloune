/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Radix.Event;

public class LevelSelect : BaseView {

    private List<UILevelInfo> mLevels;
    private int mCurrentLevelIndex = 0;

    [SerializeField]
    private Text m_Title;

    [SerializeField]
    private Text m_Subtitle;

    [SerializeField]
    private Image m_Lock;

    [SerializeField]
    private Image m_Background;

    private void Start()
    {
        mLevels = new List<UILevelInfo>();
        EventService.Register<IntDelegate>(ELevelSelectEvent.WANT_CHANGE_LEVEL, ChangeLevel);
    }

    //Late start (To change)
    private bool loaded = false;
    private void Update()
    {
        if (!loaded)
        {
            loaded = true;
            AddMockData();
            EventService.DispatchEvent(ELevelSelectEvent.LEVEL_LOADED, mLevels);
            ChangeLevel(mCurrentLevelIndex);
        }
    }

    public void OnNextClick()
    {
        if (mCurrentLevelIndex + 1 < mLevels.Count)
        {
            ChangeLevel(mCurrentLevelIndex + 1);
        }
    }

    public void OnPreviousClick()
    {
        if (mCurrentLevelIndex > 0)
        {
            ChangeLevel(mCurrentLevelIndex - 1);
        }
    }

    public void OnBackClick()
    {
        Application.LoadLevel("ChapterSelect");
    }

    public void OnStartClick()
    {
        if (mLevels [mCurrentLevelIndex].IsUnlocked)
        {
            Application.LoadLevel("Level1_" + (mCurrentLevelIndex + 1));
        }
    }

    private void ChangeLevel(int pNewLevel)
    {
        mCurrentLevelIndex = pNewLevel;

        UILevelInfo info = mLevels [mCurrentLevelIndex];

        string levelName = "Level " + info.Chapter + "-" + info.Index;

        m_Title.text = info.Name;
        m_Subtitle.text = levelName;

        LoadImage(info);

        m_Lock.enabled = !info.IsUnlocked;

        EventService.DispatchEvent(ELevelSelectEvent.LEVEL_CHANGED, mCurrentLevelIndex);
    }

    private void LoadImage(UILevelInfo pInfo)
    {
        Sprite newSprite =  Resources.Load <Sprite>("Screenshot/" + "Level" + pInfo.Chapter + "-" + pInfo.Index);
        if (pInfo.IsUnlocked && newSprite){
            m_Background.sprite = newSprite;
        } else {
            m_Background.sprite = Resources.Load <Sprite>("Screenshot/Cadre_BG");
        }
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(ELevelSelectEvent));
    }

    //TO DELETE
    private void AddMockData()
    {
        UILevelInfo level1 = new UILevelInfo();
        level1.Index = 1;
        level1.Chapter = 1;
        level1.Name = "Title1";
        level1.IsUnlocked = true;
        mLevels.Add(level1);

        UILevelInfo level2 = new UILevelInfo();
        level2.Index = 2;
        level2.Chapter = 1;
        level2.Name = "Title2";
        level2.IsUnlocked = true;
        mLevels.Add(level2);

        UILevelInfo level3 = new UILevelInfo();
        level3.Index = 3;
        level3.Chapter = 1;
        level3.Name = "Title3";
        level3.IsUnlocked = true;
        mLevels.Add(level3);

        UILevelInfo level4 = new UILevelInfo();
        level4.Index = 4;
        level4.Chapter = 1;
        level4.Name = "Title4";
        level4.IsUnlocked = true;
        mLevels.Add(level4);

        UILevelInfo level5 = new UILevelInfo();
        level5.Index = 5;
        level5.Chapter = 1;
        level5.Name = "Title5";
        level5.IsUnlocked = true;
        mLevels.Add(level5);

        UILevelInfo level6 = new UILevelInfo();
        level6.Index = 6;
        level6.Chapter = 1;
        level6.Name = "Title6";
        level6.IsUnlocked = true;
        mLevels.Add(level6);

        UILevelInfo level7 = new UILevelInfo();
        level7.Index = 7;
        level7.Chapter = 1;
        level7.Name = "Title7";
        level7.IsUnlocked = true;
        mLevels.Add(level7);

        UILevelInfo level8 = new UILevelInfo();
        level8.Index = 8;
        level8.Chapter = 1;
        level8.Name = "Title8";
        level8.IsUnlocked = true;
        mLevels.Add(level8);

        UILevelInfo level9 = new UILevelInfo();
        level9.Index = 9;
        level9.Chapter = 1;
        level9.Name = "Title9";
        level9.IsUnlocked = false;
        mLevels.Add(level9);

        UILevelInfo level10 = new UILevelInfo();
        level10.Index = 10;
        level10.Chapter = 1;
        level10.Name = "Title10";
        level10.IsUnlocked = false;
        mLevels.Add(level10);
    }
}
