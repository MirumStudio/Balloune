using UnityEngine;
using System.Collections;
using Radix.Event;

public class Bridge : MonoBehaviour {

    [SerializeField]
    public Transform mGearBox;

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
        float distance = Vector2.Distance(mGearBox.position, pPos);
        return distance <= 5;
    }

    private void DropBridge()
    {
        JointAngleLimits2D newLimits = new JointAngleLimits2D();
        newLimits.min = 0;
        newLimits.max = mHinge.limits.max;
        mHinge.limits = newLimits;
    }
}
