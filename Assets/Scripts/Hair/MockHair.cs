using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MockHair : MonoBehaviour, IHairFolicule
{
    [SerializeField] private SphereCollider m_Collider = default;
    [SerializeField] private Transform m_Base;
    [SerializeField] private Transform m_Tip;
    [SerializeField] private float m_YScale;
    [SerializeField] private float m_XZScale;
    [SerializeField,ReadOnly]
    private float m_Height;
    public float height
    {
        get => m_Height;
        set
        {
            m_Height = value;
            UpdateVisual(m_Height,m_Tangent);
        }
    }
    [SerializeField,ReadOnly]
    private Vector2 m_Tangent;

    public Vector2 tangent
    {

        get => m_Tangent;
        set
        {
            m_Tangent = value;
            UpdateVisual(m_Height, m_Tangent);
        }
    }

    public Vector3 position() => transform.position;
    public float radius() => m_Collider.radius;

    void Start()
    {
        UpdateVisual(0, Vector2.zero);
    }
    
    void UpdateVisual(float height, Vector2 tangent)
    {
        tangent *= m_XZScale;
        m_Tip.position = m_Base.position + new Vector3(tangent.x, height * m_YScale,tangent.y);
    }
}
