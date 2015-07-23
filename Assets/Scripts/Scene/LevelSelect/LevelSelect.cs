﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    private List<UILevelInfo> mLevels;
    private int mCurrentLevelIndex = 0;

    [SerializeField]
    private Text m_Title;

    [SerializeField]
    private Text m_Subtitle;

    [SerializeField]
    private Image m_Lock;

    private void Start()
    {
        mLevels = new List<UILevelInfo>();
        AddMockData();
        ChangeLevel(mCurrentLevelIndex);
    }

    public void OnNextClick()
    {
        if (mCurrentLevelIndex + 1 < mLevels.Count)
        {
            ChangeLevel(mCurrentLevelIndex +1);
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

        m_Title.text = info.Name;
        m_Subtitle.text = "Level " + info.Chapter + "-" + info.Index;

        m_Lock.enabled = !info.IsUnlocked;

    }

    //TO DELETE
    private void AddMockData()
    {
        UILevelInfo level1 = new UILevelInfo();
        level1.Index = 1;
        level1.Chapter = 1;
        level1.Name = "Tittle1";
        level1.IsUnlocked = true;
        mLevels.Add(level1);

        UILevelInfo level2 = new UILevelInfo();
        level2.Index = 2;
        level2.Chapter = 1;
        level2.Name = "Tittle2";
        level2.IsUnlocked = true;
        mLevels.Add(level2);

        UILevelInfo level3 = new UILevelInfo();
        level3.Index = 3;
        level3.Chapter = 1;
        level3.Name = "Tittle3";
        level3.IsUnlocked = true;
        mLevels.Add(level3);

        UILevelInfo level4 = new UILevelInfo();
        level4.Index = 4;
        level4.Chapter = 1;
        level4.Name = "Tittle4";
        level4.IsUnlocked = true;
        mLevels.Add(level4);

        UILevelInfo level5 = new UILevelInfo();
        level5.Index = 5;
        level5.Chapter = 1;
        level5.Name = "Tittle5";
        level5.IsUnlocked = true;
        mLevels.Add(level5);

        UILevelInfo level6 = new UILevelInfo();
        level6.Index = 6;
        level6.Chapter = 1;
        level6.Name = "Tittle6";
        level6.IsUnlocked = true;
        mLevels.Add(level6);

        UILevelInfo level7 = new UILevelInfo();
        level7.Index = 7;
        level7.Chapter = 1;
        level7.Name = "Tittle7";
        level7.IsUnlocked = false;
        mLevels.Add(level7);

        UILevelInfo level8 = new UILevelInfo();
        level8.Index = 8;
        level8.Chapter = 1;
        level8.Name = "Tittle8";
        level8.IsUnlocked = false;
        mLevels.Add(level8);

        UILevelInfo level9 = new UILevelInfo();
        level9.Index = 9;
        level9.Chapter = 1;
        level9.Name = "Tittle9";
        level9.IsUnlocked = false;
        mLevels.Add(level9);

        UILevelInfo level10 = new UILevelInfo();
        level10.Index = 10;
        level10.Chapter = 1;
        level10.Name = "Tittle10";
        level10.IsUnlocked = false;
        mLevels.Add(level10);
    }
}
