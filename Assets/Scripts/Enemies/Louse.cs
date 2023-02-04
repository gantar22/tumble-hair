using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILouse
{
    public bool isAlive { get; }

    public void Spawn(Vector3 position);
}

public class Louse : MonoBehaviour, ILouse
{
    [SerializeField]
    private AudioClip m_DeathSFX;
    [SerializeField]
    private UnityEvent<Vector3> m_OnDeath;
    public UnityEvent<Vector3> OnDeath => m_OnDeath;

    public bool isAlive { get; private set; }

    public void Spawn(Vector3 position)
    {
        
    }
    
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
