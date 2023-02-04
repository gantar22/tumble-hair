using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private IHairFolicule[] m_Folicules = default;
    private IHairFolicule[] folicules => m_Folicules;
}
