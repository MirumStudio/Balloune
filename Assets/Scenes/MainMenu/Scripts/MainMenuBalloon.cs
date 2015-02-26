using Radix.Error;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuBalloon : MonoBehaviour {

    public EventTrigger.Entry OnClick;

    private LineRenderer mLinerenderer;

    private void Start()
    {
        mLinerenderer = GetComponent<LineRenderer>();
        UpdateLineRendererPosition();
        mLinerenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y - 1000, 0));
	}

    private void Update()
    {
        UpdateLineRendererPosition();
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick.callback.Invoke(null);
        }
    }

    private void UpdateLineRendererPosition()
    {
        mLinerenderer.SetPosition(0, transform.position);
    }
}
