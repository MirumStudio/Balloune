using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    private static int mChildCount;

	// Use this for initialization
	void Start () {
        mChildCount = transform.FindChild("Kids").childCount;
	}
	
    public static int ChildCount
    {
        get { return mChildCount; }
    }

}
