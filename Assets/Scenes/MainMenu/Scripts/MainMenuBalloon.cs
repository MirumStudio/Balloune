using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenuBalloon : MonoBehaviour {

    public EventTrigger.Entry OnClick;

    private LineRenderer mLinerenderer;

	void Start () {
        mLinerenderer = GetComponent<LineRenderer>();
        mLinerenderer.SetPosition(0, transform.position);
        mLinerenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y - 1000, 0));
	}
	
	void Update () {
        mLinerenderer.SetPosition(0, transform.position);
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick.callback.Invoke(null);
        }
    }
}
