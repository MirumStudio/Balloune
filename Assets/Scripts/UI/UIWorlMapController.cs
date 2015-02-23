using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class UIWorlMapController : MonoBehaviour {

    public void Start()
    {
        EventListener.Register(EWorldMapEvent.BEGIN_CHANGE_LEVEL, b);
        EventListener.Register(EWorldMapEvent.END_CHANGE_LEVEL, e);
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

    public void b(Enum lol, object lold)
    {
        FadeOut();
    }

    public void e(Enum lol, object lold)
    {
        ChangeLevel(lold as LevelPoint);
    }
}
