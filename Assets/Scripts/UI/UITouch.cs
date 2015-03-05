using UnityEngine;
using System.Collections;

//TODO: Find a better name
public class UITouch : MonoBehaviour {
	void Start () {
        gameObject.SetActive(Input.touchSupported);
	}
}
