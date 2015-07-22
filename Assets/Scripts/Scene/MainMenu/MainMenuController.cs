/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayClick()
    {
        //TODO : Go to load game screen
        Application.LoadLevel("ChapterSelect");
    }

    public void OnAchievementClick()
    {
       //TODO: Create Achievement View
    }

    public void OnOptionClick()
    {
        Application.LoadLevel("OptionMenuView");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void VincentLevelClick()
    {
        Application.LoadLevel("VinceLevels");
    }

	public void PocLevelClick()
	{
		Application.LoadLevel("POCLevels");
	}
}
