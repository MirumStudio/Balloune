using UnityEngine;
using System.Collections;

public class RotationLock : MonoBehaviour {

	[SerializeField]
	private Vector3 m_Rotation;
	private Quaternion mLockedRotation;
	void Start()
	{
		this.mLockedRotation=Quaternion.Euler(this.m_Rotation);
	}
	void Update () 
	{
		this.transform.rotation=this.mLockedRotation;
	}
}
