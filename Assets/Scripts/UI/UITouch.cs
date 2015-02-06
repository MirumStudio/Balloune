using UnityEngine;
using System.Collections;

public class UITouch : MonoBehaviour {
	void Start () {
        gameObject.SetActive(Input.touchSupported);
	}
}
