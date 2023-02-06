using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairSystem : MonoBehaviour
{
    [SerializeField] private IHairFolicule[] m_Hairs = default;

    private void Awake()
    {
        m_Hairs = GetComponentsInChildren<IHairFolicule>();
    }
    
    
}
