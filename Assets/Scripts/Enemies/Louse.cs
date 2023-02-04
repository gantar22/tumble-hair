using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Louse : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_DeathSFX;
    [SerializeField]
    private UnityEvent<Vector3> m_OnDeath;
    public UnityEvent<Vector3> OnDeath => m_OnDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        m_OnDeath?.Invoke(transform.position);
    }

}
