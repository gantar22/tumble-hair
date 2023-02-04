using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour, IHand
{
    [SerializeField] private Animator m_Animator = default;
    [SerializeField] private Transform m_Root = default;
    [SerializeField] private Collider m_DangerZone = default;
    private Vector3? target = null;

    private void Awake()
    {
        m_DangerZone.enabled = false;
    }

    public void Summon(Vector3 inTarget)
    {
        target = inTarget;
        m_Root.position = GetBaseLocation(inTarget);
        m_Root.gameObject.SetActive(true);
        StartCoroutine(Scratch(inTarget));
    }

    public void UnSummon()
    {
        if (target.HasValue)
        {
            var oldTarget = target.Value;
            target = null;
            m_DangerZone.enabled = false;
            StopAllCoroutines();
            m_Animator.SetBool("scratch",false);
            StartCoroutine(Retreat(GetBaseLocation(oldTarget)));
        }
    }

    Vector3 GetBaseLocation(Vector3 inTarget)
    {
        if (HandManager.I)
            return (inTarget - HandManager.I.mapCenter.position).normalized * HandManager.I.mapRadius;
        
        return Vector3.zero;
    }
    
    IEnumerator GoTo(Vector3 inTarget)
    {
        var vel = Vector3.zero;
        var rootFromDZ = m_Root.position - m_DangerZone.transform.position;
        var rootTarget = inTarget + rootFromDZ;
        while (Vector3.Distance(m_Root.position, rootTarget) > .25f)
        {
            m_Root.position = Vector3.SmoothDamp(m_Root.position,rootTarget,ref vel,.1f,2,Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator Retreat(Vector3 inTarget)
    {
        yield return GoTo(inTarget);
        m_Root.gameObject.SetActive(false);
    }
    
    IEnumerator Scratch(Vector3 inTarget)
    {
        yield return GoTo(inTarget);
        m_DangerZone.enabled = true;
        m_Animator.SetBool("scratch",true);
    }
    
    public bool activeTarget(out Vector3 outTarget)
    {
        if (target != null)
        {
            outTarget = target.Value;
            return true;
        }

        outTarget = default;
        return false;
    }
}
