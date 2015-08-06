/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using Radix.Logging;
using UnityEngine;

/*this code come from Unity demo project*/

/// <summary>
/// This script allow smooth following of the attached object to the target object on selected axis. On a camera to the main character for example.
/// </summary>
public class AutoFollow : MonoBehaviour {

	[SerializeField]
	private Transform m_Target = null;
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
    [SerializeField]
    private float m_MaxLeftX = 0.0f;
    [SerializeField]
    private float m_MaxRightX = 0.0f;

	void Start ()
	{
		if(this.m_Target==null)
		{
			Error.Create("No target is assigned to the AutoFollow : desactivating the script");
			this.enabled=false;
		}
		else if(m_Smoothness<=0)
		{
			Log.Warning("The smoothness value of AutoFollow is zero or lower. Consider desactivating the script instead.", ELogCategory.GAMEPLAY);
		}
		if(!(m_FollowX||m_FollowY||m_FollowZ))
		{
            Log.Warning("The AutoFollow do not use any axis, consider desactivating the script.", ELogCategory.GAMEPLAY);
		}
	}

	void Update () 
	{
		Vector3 target = this.transform.position;
		if(this.m_FollowX)
		{
            float x = this.m_Target.position.x;
            if(x < m_MaxLeftX)
            {
                x = m_MaxLeftX;
            }
            else if (x > m_MaxRightX)
            {
                x = m_MaxRightX;
            }
			target.x = x;
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
