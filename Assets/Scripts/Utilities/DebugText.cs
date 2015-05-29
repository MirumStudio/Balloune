using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    private static Text Instance;

	void Start () {
        Instance = GetComponent<Text>();
	}

    public static void Log(string pMessage)
    {
        Log(pMessage, Color.black);
    }

    public static void Log(string pMessage, Color pColor)
    {
        if (Instance != null)
        {
            Instance.text = pMessage;
            Instance.color = pColor;
        }
    }
}
