using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    private static int mChildCount;

	void Start () {
        //mChildCount = transform.FindChild("Kids").childCount;
        mChildCount = GetComponentsInChildren<Interactable>().Length;
	}
	
    public static int ChildCount
    {
        get { return mChildCount; }
    }

}
