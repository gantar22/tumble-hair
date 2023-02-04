using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager I;
    public IHand[] hands = default;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Summon(Vector3 location, float duration)
    {
        var hand = hands.FirstOrDefault(_ => _.isActive);
        if(hand != null)
            StartCoroutine(SummonImpl(location,hand,duration));
    }

    IEnumerator SummonImpl(Vector3 location,IHand hand,float duration)
    {
        hand.Summon(location);
        yield return new WaitForSeconds(duration);
        hand.UnSummon();
    }
    
    private void OnValidate()
    {
        hands = GetComponentsInChildren<IHand>();
    }
}
