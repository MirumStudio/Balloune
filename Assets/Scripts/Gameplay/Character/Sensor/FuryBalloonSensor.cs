using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Logging;

public class FuryBalloonSensor : CharacterSensor {

    protected const string GIRL_BALLOON_LAYER_NAME = "GirlBalloon"; 
    protected const string NOT_GIRL_BALLOON_LAYER_NAME = "NotGirlBalloon";

    private const string BALLOON_IDENTIFIER = "Balloon";
	
    private const string FURY_PARAMATER = "IsInFury";

    [SerializeField]
    private float m_SensorRange = 3.0f;

	void Update () {
        // (!mAnimator.GetBool(FURY_PARAMATER))
        {
            Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(transform.position, m_SensorRange, BalloonLayerMask);

            bool haveBalloon = false;

            foreach (Collider2D collider2d in touchedColliders)
            { 
                RaycastHit2D hit = Physics2D.Linecast(transform.position, collider2d.transform.position, GroundLayerMask);

                haveBalloon = !hit;
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
}
