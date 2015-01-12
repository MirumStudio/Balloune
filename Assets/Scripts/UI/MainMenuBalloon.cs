using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public delegate void LOL();
public class MainMenuBalloon : MonoBehaviour, IPointerDownHandler
{
    public EventTrigger.Entry OnClick;

    void Start()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.position.x, transform.position.y - 1000, 0));
    }

    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      // Application.LoadLevel("WorldMapView");
        //For touch device ?
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
           OnClick.callback.Invoke(null);
        }
    }
}
