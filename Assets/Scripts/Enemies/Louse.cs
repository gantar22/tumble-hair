using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILouse
{
    public bool isAlive { get; }

    public void Spawn(Vector3 position);
    public Vector3 position { get; }
}

public class Louse : MonoBehaviour, ILouse
{
    [SerializeField]
    private AudioClip m_DeathSFX;
    [SerializeField]
    private UnityEvent<Vector3> m_OnDeath;
    public UnityEvent<Vector3> OnDeath => m_OnDeath;

    public bool isAlive { get => gameObject.activeSelf;}

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public Vector3 position => transform.position;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AudioManager.I.PlayOneShot(m_DeathSFX);
            gameObject.SetActive(false);
            m_OnDeath?.Invoke(transform.position);
            m_OnDeath.RemoveAllListeners();
        }
    }
}
