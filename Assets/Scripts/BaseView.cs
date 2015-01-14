using UnityEngine;
using System.Collections;
using Radix.Service;

public class BaseView : MonoBehaviour {

   /* [SerializeField]
    private BaseController m_Controller = null;*/

	// Use this for initialization
	virtual protected void Start () {
        ServiceManager.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
