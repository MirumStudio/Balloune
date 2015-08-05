using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolSensor : CharacterSensor {

    private List<Vector2> mPatrolPoints = new List<Vector2>();

    private int mCurrentIndex = 0;

    public EdgeCollider2D edge;

	// Use this for initialization
	void Start () {

        base.Start();

        for(int i = 0; i < edge.points.Length; i++)
        {
            Vector2 worldSpacePoint = transform.TransformPoint(edge.points[i]);
            mPatrolPoints.Add (worldSpacePoint);
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (Vector2.Distance(mPatrolPoints [mCurrentIndex], transform.position) < 0.2f)
        {
            if(mCurrentIndex + 1 == mPatrolPoints.Count)
            {
                mCurrentIndex = 0;
            }
            else
            {
                mCurrentIndex ++;
            }
        }

        if (mPatrolPoints[mCurrentIndex].x > transform.position.x)
        {
            UpdateSpeedParamater(0.3f);
        }
        else if (mPatrolPoints [mCurrentIndex].x < transform.position.x)
        {
            UpdateSpeedParamater(-0.3f);
        }

	}

    private void UpdateSpeedParamater(float pSpeed)
    {
        mAnimator.SetFloat(SPEED_PARAMATER, pSpeed);
    }
}
