using UnityEngine;
using System.Collections;
using Radix.Event;

public class LevelPoint : MonoBehaviour {

    [SerializeField]
    private string m_LevelName = string.Empty;

    public int ID
    {
        get { return System.Int32.Parse(name.Substring(name.Length - 1)); }
    }

    public string LevelName
    {
        get { return m_LevelName; }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventService.DispatchEvent(EWorldMapEvent.WANT_CHANGE_LEVEL, ID);
        }
    }
}
