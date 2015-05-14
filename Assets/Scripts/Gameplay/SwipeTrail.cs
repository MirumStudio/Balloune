using UnityEngine;
using System.Collections;

public class SwipeTrail : MonoBehaviour {

    private TrailRenderer mTrailRenderer;
    private float mTrailRendDefaultTime;
    private float mDragEndTime;

    void Start()
    {
        mTrailRenderer = GetComponent<TrailRenderer>();
        mTrailRendDefaultTime = mTrailRenderer.time;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector3 position;
            position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);//get position, where touch is detected
            position.z = -1;

            transform.position = position;

            //wait little bit before showing trail renderer. used to not draw trail at first click/touch, 
            //because this object jumps from last click/touch position to current click/touch position and it'll look weird
            if (Time.time > mDragEndTime + 0.05f)
            {
                mTrailRenderer.time = mTrailRendDefaultTime;
            }
        }
        else
        {
            mTrailRenderer.time = 0;
            mDragEndTime = Time.time;
        }
    }
}
