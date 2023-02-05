using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairBone : MonoBehaviour
{
    [SerializeField] private float m_RandomMin;
    [SerializeField] private float m_RandomMax;
    private float m_Max;
    public float Max => m_Max;

    private void Start()
    {
        m_Max = Random.Range(m_RandomMin,m_RandomMax);
    }
}
