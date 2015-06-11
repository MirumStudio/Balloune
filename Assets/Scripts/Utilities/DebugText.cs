/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    public GameObject m_Panel;
    public Text m_DebugText;
    public Text m_ButtonText;

    private bool mIsHidding = true;

    private static DebugText Instance = null;

    void Awake()
    {
        Instance = this;
    }

	void Start () {
        if(Debug.isDebugBuild)
        {
            UpdateUI();
        }
        else
        {
            gameObject.SetActive(false);
        }
	}

    public static void Log(string pMessage)
    {
        if (Instance != null && Debug.isDebugBuild && !string.IsNullOrEmpty(pMessage))
        {
            Instance.AddText(Time.realtimeSinceStartup.ToString() + "   " + pMessage);
            Instance.EndLine();
        }
    }

    private void AddText(string pMessage)
    {
        Instance.m_DebugText.text += pMessage;
    }

    private void EndLine()
    {
        AddText("\n");
    }

    public void OnButtonClick()
    {
        mIsHidding = !mIsHidding;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Panel.SetActive(!mIsHidding);
        m_ButtonText.text = mIsHidding ? "Show Console" : "Hide Console";
    }

    public void onClearClick()
    {
        m_DebugText.text = string.Empty;
    }
}
