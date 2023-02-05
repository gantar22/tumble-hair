using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour, IHand
{
    [SerializeField] private Animator m_Animator = default;
    [SerializeField] private Transform m_Root = default;
    [SerializeField] private Collider m_DangerZone = default;
    [SerializeField] private Renderer m_Renderer = default;
    [SerializeField] private MasterTuningSO tuningAsset = default;
    private Vector3? target = null;
    public bool scratching { get; private set; }
    private void Awake()
    {
        m_DangerZone.enabled = false;
        SetVisibility(false);
    }

    void SetVisibility(bool inValue)
    {
        m_Renderer.enabled = inValue;
        m_Root.gameObject.SetActive(inValue);
    }

    void SetScratching(bool inValue)
    {
        m_Animator.SetBool("scratch",inValue);
        m_DangerZone.enabled = inValue;
        scratching = inValue;
    }
    public void Summon(Vector3 inTarget)
    {
        target = inTarget;
        m_Root.position = GetBaseLocation(inTarget, out var rot);
        m_Root.rotation = rot;
        SetVisibility(true);
        StartCoroutine(Scratch(inTarget));
    }

    public void UnSummon()
    {
        if (target.HasValue)
        {
            var oldTarget = target.Value;
            SetScratching(false);
            StopAllCoroutines();
            var baseLocation = GetBaseLocation(oldTarget, out var _);
            StartCoroutine(Retreat(baseLocation));
        }
    }

    Vector3 GetBaseLocation(Vector3 inTarget, out Quaternion outRotation)
    {
        if (HandManager.I)
        {
            var dir = (inTarget - HandManager.I.mapCenter.position).normalized;
            outRotation = Quaternion.LookRotation(-dir);
            return dir * HandManager.I.mapRadius;
        }

        outRotation = default;
        return Vector3.zero;
    }
    
    IEnumerator GoTo(Vector3 inTarget)
    {
        var vel = Vector3.zero;
        var rootFromDZ = m_Root.position - m_DangerZone.transform.position;
        var rootTarget = inTarget + rootFromDZ;
        while (Vector3.Distance(m_Root.position, rootTarget) > .25f)
        {
            m_Root.position = Vector3.SmoothDamp(m_Root.position,rootTarget,ref vel,.1f,tuningAsset.handSpeed,Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Retreat(Vector3 inTarget)
    {
        yield return GoTo(inTarget);
        SetVisibility(false);
        target = null;
    }
    
    IEnumerator Scratch(Vector3 inTarget)
    {
        yield return GoTo(inTarget);
        yield return new WaitForSeconds(tuningAsset.handPauseTime);
        SetScratching(true);
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
