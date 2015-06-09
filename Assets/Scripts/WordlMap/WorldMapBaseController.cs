/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public class WorldMapBaseController : MonoBehaviour {

    private const string LEVEL_FORMAT = "Level{0}_{1}";

    [SerializeField]
    private int m_Id = 1;

    public void OnExitClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }

    public void OnPlayClick()
    {
        string level = string.Format("Level{0}_{1}", m_Id, (GetComponentInChildren<WordlMapCharacter>().CurrentLevel+1));
        Application.LoadLevel(level);
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(EWorldMapEvent));
    }
}
