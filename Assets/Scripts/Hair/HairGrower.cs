using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairGrower : MonoBehaviour
{
    [SerializeField] private SphereCollider m_Collider = default;
    private HashSet<IHairFolicule> m_MovedFolicules = new HashSet<IHairFolicule>();
    [SerializeField] private float m_GrowthSpeed = 1;
    private void OnTriggerEnter(Collider other)
    {
        var folicule = other.GetComponent<IHairFolicule>(); 
        if (folicule != null)
        {
            m_MovedFolicules.Add(folicule);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        var folicule = other.GetComponent<IHairFolicule>(); 
        if (folicule != null)
        {
            m_MovedFolicules.Remove(folicule);
        }
        
    }

    private void Update()
    {
        foreach (var fol in m_MovedFolicules)
        {
            var dif = (fol.position() - m_Collider.transform.position);
            fol.tangent = new Vector2(dif.x,dif.z).normalized * Mathf.Lerp(1,0, dif.magnitude / (m_Collider.radius - fol.radius()));
            
            fol.height =
                Mathf.Clamp01(fol.height + Time.deltaTime * Vector3.Distance(fol.position(),m_Collider.transform.position) * m_GrowthSpeed);
        }
    }
}
