using UnityEngine;
using System.Collections;
using Radix.Event;

public class LevelPoint : MonoBehaviour {

    [SerializeField]
    private string m_LevelName = string.Empty;

    [SerializeField]
    private bool m_IsUnlock = true;

    void Start()
    {
        if(!m_IsUnlock)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

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
        if (m_IsUnlock && Input.GetMouseButtonDown(0))
        {
            EventService.DispatchEvent(EWorldMapEvent.WANT_CHANGE_LEVEL, ID);
        }
    }
}
