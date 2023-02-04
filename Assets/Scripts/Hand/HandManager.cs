using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private MasterTuningSO tuningAsset = default;
    public Transform mapCenter = default;
    public float mapRadius = default;
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

    public bool Summon(Vector3 target)
    {
        var hand = hands.FirstOrDefault(_ => _.activeTarget(out var _));
        if (hand == null)
            return false;
        if (hands.Any(_ =>
                _.activeTarget(out var existingTarget) &&
                Vector3.Distance(existingTarget, target) < tuningAsset.handZoneRadius))
            return false;
        StartCoroutine(SummonImpl(target,hand,tuningAsset.handScratchDuration));
        return true;
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
