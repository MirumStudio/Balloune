using UnityEngine;
using System.Collections;
using Radix.Event;

public class LevelPoint : MonoBehaviour {

    public int ID
    {
        get { return System.Int32.Parse(name.Substring(name.Length - 1)); }
    }

    public string LevelName
    {
        get { return name; }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventService.DipatchEvent(EWorldMapEvent.WANT_CHANGE_LEVEL, ID);
        }
    }
}
