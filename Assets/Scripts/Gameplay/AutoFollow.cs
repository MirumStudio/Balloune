using UnityEngine;
using System.Collections;
/// <summary>
/// This script allow smooth following of the attached object to the target object on selected axis. On a camera to the main character for example.
/// </summary>
public class AutoFollow : MonoBehaviour {

	[SerializeField]
	private Transform m_Target;
	[SerializeField]
	private float m_Smoothness = 0.5f; //An higher value means a tighter follow.
	[SerializeField]
	private bool m_FollowX = false;
	[SerializeField]
	private bool m_FollowY = false;
	[SerializeField]
	private bool m_FollowZ = false;
    [SerializeField]
    private float m_AjustY = 0.0f;
	void Start ()
	{
		if(this.m_Target==null)
		{
			Debug.LogError("No target is assigned to the AutoFollow : desactivating the script");
			this.enabled=false;
		}
		else if(m_Smoothness<=0)
		{
			Debug.LogWarning("The smoothness value of AutoFollow is zero or lower. Consider desactivating the script instead.");
		}
		if(!(m_FollowX||m_FollowY||m_FollowZ))
		{
			Debug.LogWarning("The AutoFollow do not use any axis, consider desactivating the script.");
		}
	}
	// Update is called once per frame
	void Update () 
	{
		Vector3 target = this.transform.position;
		if(this.m_FollowX)
		{
			target.x = this.m_Target.position.x;
		}
		if(this.m_FollowY)
		{
			target.y = this.m_Target.position.y + m_AjustY;
		}
		if(this.m_FollowZ)
		{
			target.z = this.m_Target.position.z;
		}
		this.transform.position=Vector3.Lerp(this.transform.position,target,this.m_Smoothness*Time.deltaTime);
	}
}
