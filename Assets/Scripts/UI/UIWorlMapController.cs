using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;
using Radix.Error;

public class UIWorlMapController : MonoBehaviour {

    public void Start()
    {
        EventListener.Register(EWorldMapEvent.BEGIN_CHANGE_LEVEL, OnBeginChangeLevel);
        EventListener.Register(EWorldMapEvent.END_CHANGE_LEVEL, OnEndChangeLevel);
    }

    public void FadeOut()
    {
        //TODO: Real Fade Out
        GetComponentInParent<CanvasGroup>().alpha = 0f;
    }

    public void FadeIn()
    {
        //TODO: Real Fade In
        GetComponentInParent<CanvasGroup>().alpha = 1f;
    }

    public void ChangeLevel(LevelPoint pLevel)
    {
        var text = transform.FindChild("Title").GetComponent<Text>();
        text.text = pLevel.LevelName;
        FadeIn();
    }

    public void OnBeginChangeLevel(Enum pEvent, object pArgs)
    {
        FadeOut();
    }

    public void OnEndChangeLevel(Enum pEvent, object pArgs)
    {
		Assert.Check(pArgs is LevelPoint);
        ChangeLevel(pArgs as LevelPoint);
    }
}
