using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonOnPressed : MonoBehaviour {

    private bool isPressed = false;

	// Use this for initialization
	void Start () {
	    Button button = GetComponent<Button>();
       // button.
	}
	
	// Update is called once per frame
	void Update () {

        /*Debug.Log(Input.GetTouch(0).ToString());
        if(isPressed)
        {
            isPressed = Input.GetMouseButton(0);
            Input.GetTouch(0);
            Debug.Log("Pressed: " + isPressed);
        }*/

        if (Input.GetMouseButton(0))
            Debug.Log("Pressed left clickdfvg.");
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed left click.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
        //Debug.Log(Input.touchCount.ToString());
        GetComponentInChildren<Text>().text = "nothing";
	    foreach(Touch touch in Input.touches)
        {
            GetComponentInChildren<Text>().text = "foreach";
            if(touch.position.x > transform.position.x)
            {
                Debug.Log("Allo !~");
            }
        }

        if (Input.touchCount == 1)
        {
            GetComponentInChildren<Text>().text = "touchcount";
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                GetComponentInChildren<Text>().text = "countok";
                Debug.Log("Allo !13213456~");

            }
        }
	}

    public void OnButtonClick()
    {
        isPressed = true;
    }
}
