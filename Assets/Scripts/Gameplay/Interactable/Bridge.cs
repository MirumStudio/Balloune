using UnityEngine;
using System.Collections;
using Radix.Event;

public class Bridge : MonoBehaviour {
	
	private const int AREA_OF_EFFECT = 3;

    [SerializeField]
    public Transform m_GearBox;

    private HingeJoint2D mHinge;

	void Start () {
        mHinge = GetComponent<HingeJoint2D>();
        EventService.Register<Vector2Delegate>(EGameEvent.STUN_BALLOON_POP, OnStunBalloonPop);
	}

    private void OnStunBalloonPop(Vector2 pPos)
    {
        if(IsNearGearBox(pPos))
        {
            DropBridge();
        }
    }

    private bool IsNearGearBox(Vector2 pPos)
    {
        float distance = Vector2.Distance(m_GearBox.position, pPos);
		return distance <= AREA_OF_EFFECT;
    }

    private void DropBridge()
    {
        JointAngleLimits2D newLimits = new JointAngleLimits2D();
        newLimits.min = 0;
        newLimits.max = mHinge.limits.max;
        mHinge.limits = newLimits;
    }
}
