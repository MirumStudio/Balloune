using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Logging;

public class FuryBalloonSensor : CharacterSensor {

    protected const string GIRL_BALLOON_LAYER_NAME = "GirlBalloon"; 
    protected const string NOT_GIRL_BALLOON_LAYER_NAME = "NotGirlBalloon";

    private const string BALLOON_IDENTIFIER = "Balloon";
	
    private const string FURY_PARAMATER = "IsInFury";
    private const string BALLOON_DISTANCE_PARAMATER = "DistanceFromBalloon";

    [SerializeField]
    private float m_SensorRange = 5.0f;

    //find ballloon that is nearest
	void Update () {
        // (!mAnimator.GetBool(FURY_PARAMATER))
        {
            Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(transform.position, m_SensorRange, BalloonLayerMask);

            bool haveBalloon = false;

            foreach (Collider2D collider2d in touchedColliders)
            { 
                RaycastHit2D hit = Physics2D.Linecast(transform.position, collider2d.transform.position, GroundLayerMask);

                if(!hit)
                {
                    haveBalloon = true;
                    mAnimator.SetFloat(BALLOON_DISTANCE_PARAMATER,Vector2.Distance(collider2d.transform.position, transform.position));
                    if (collider2d.transform.position.x > transform.position.x)
                    {
                        UpdateSpeedParamater(1f);
                    }
                    else if (collider2d.transform.position.x < transform.position.x)
                    {
                        UpdateSpeedParamater(-1f);
                    }

                    break;
                }
            }

            mAnimator.SetBool(FURY_PARAMATER,haveBalloon);
        }
	}

    protected int BalloonLayerMask
    {
        get
        {
            return (1 << LayerMask.NameToLayer(GIRL_BALLOON_LAYER_NAME)) | (1 << LayerMask.NameToLayer(NOT_GIRL_BALLOON_LAYER_NAME));
        }
    }

    private void UpdateSpeedParamater(float pSpeed)
    {
        mAnimator.SetFloat(SPEED_PARAMATER, pSpeed);
    }
}
