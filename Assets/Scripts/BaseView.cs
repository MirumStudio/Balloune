using UnityEngine;
using System.Collections;
using Radix.Service;

public class BaseView : MonoBehaviour {

    static public string SCENE_NAME = "NONE";

    virtual protected void Start()
    {
        ServiceManager.Instance.Init();
    }
}
