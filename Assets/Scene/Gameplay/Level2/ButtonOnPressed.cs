using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOnPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool isPressed = false;
    public bool buttonHeld;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonHeld = false;
    }

	// Use this for initialization
	void Start () {
	    Button button = GetComponent<Button>();
        //button.OnPointerDown.AddListener(lol);
       // button.
	}
	void lol()
    {

    }
	// Update is called once per frame
	void Update () {
        if (buttonHeld)
        {
            Debug.Log("GOO");
        }

        /*Debug.Log(Input.GetTouch(0).ToString());
        if(isPressed)
        {
            isPressed = Input.GetMouseButton(0);
            Input.GetTouch(0);
            Debug.Log("Pressed: " + isPressed);
        }*/
        /*GetComponentInChildren<Text>().text = "nothing";
        if (Input.GetMouseButton(0))
            GetComponentInChildren<Text>().text = "but0";
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            GetComponentInChildren<Text>().text = "but 1";
        }

        if (Input.G()etMouseButtonDown(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
        //Debug.Log(Input.touchCount.ToString());
      
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
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                GetComponentInChildren<Text>().text = "countok";
                Debug.Log("Allo !13213456~");

            }
        }*/

        if (GetComponent<Button>().spriteState.pressedSprite != null)
        {
            Debug.Log("YIOOO");
        }

        //GetComponentInChildren<Text>().text = "->";
        foreach (Touch touch in Input.touches)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                // GetComponentInChildren<Text>().text = "countok";
            }
        }
	}

    public void OnButtonClick()
    {
        isPressed = true;
    }
}
