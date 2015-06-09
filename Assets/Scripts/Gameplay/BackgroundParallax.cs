/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
/* This code come from Unity 2d Demo project*/

public class BackgroundParallax : MonoBehaviour
{
	public Transform[] mBackgrounds;				// Array of all the backgrounds to be parallaxed.
	public float mParallaxScale;					// The proportion of the camera's movement to move the backgrounds by.
	public float mParallaxReductionFactor;		// How much less each successive layer should parallax.
	public float mSmoothing;						// How smooth the parallax effect should be.


	private Transform mCam;						// Shorter reference to the main camera's transform.
	private Vector3 mPreviousCamPos;				// The postion of the camera in the previous frame.

    private bool mFirstUpdate = true;

	void Awake ()
	{
		// Setting up the reference shortcut.
		mCam = Camera.main.transform;
	}


	void Start ()
	{
		// The 'previous frame' had the current frame's camera position.
		mPreviousCamPos = mCam.position;
	}


    void Update()
    {
        if (mFirstUpdate)
        {
            mFirstUpdate = false;
            mPreviousCamPos = mCam.position;
        }
        else
        {
            // The parallax is the opposite of the camera movement since the previous frame multiplied by the scale.
            float parallax = (mPreviousCamPos.x - mCam.position.x) * mParallaxScale;

            // For each successive background...
            for (int i = 0; i < mBackgrounds.Length; i++)
            {
                // ... set a target x position which is their current position plus the parallax multiplied by the reduction.
                float backgroundTargetPosX = mBackgrounds[i].position.x + parallax * (i * mParallaxReductionFactor + 1);

                // Create a target position which is the background's current position but with it's target x position.
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, mBackgrounds[i].position.y, mBackgrounds[i].position.z);

                // Lerp the background's position between itself and it's target position.
                mBackgrounds[i].position = Vector3.Lerp(mBackgrounds[i].position, backgroundTargetPos, mSmoothing * Time.deltaTime);
            }

            // Set the previousCamPos to the camera's position at the end of this frame.
            mPreviousCamPos = mCam.position;
        }
    }
}
