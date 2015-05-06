using UnityEngine;
using System.Collections;
using Radix.Event;
using System;
using Radix.Error;

public class Lifebar : MonoBehaviour {

    private int m_lifeCount;

	void Start () {
        m_lifeCount = transform.childCount;
        EventListener.Register(EGameEvent.LIFE_COLLISION, OnLifeCollision);
	}

    private void OnLifeCollision(Enum pEvent, System.Object pArg)
    {
        Assert.Check(pArg is LifeInteractable);
        int life = (pArg as LifeInteractable).Life;
        (pArg as LifeInteractable).gameObject.SetActive(false);

        while (life > 0)
        {
            AddOneLife();
            life--;
        }
    }

    private bool AddOneLife()
    {
        if (m_lifeCount < transform.childCount)
        {
            transform.GetChild(m_lifeCount).gameObject.SetActive(true);
            m_lifeCount++;
            return true;
        }
        return false;
    }

    private void CheckLife()
    {
        if (m_lifeCount <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }
    }
}
