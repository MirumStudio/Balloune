using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    private static int mChildCount;

	void Start () {
        mChildCount = GetComponentsInChildren<KidInteractable>().Length;
	}
	
    public static int ChildCount
    {
        get { return mChildCount; }
    }

}
