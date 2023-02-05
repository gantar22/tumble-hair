using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILouse
{
    public bool isAlive { get; }

    public void Spawn(Vector3 position);
    public Vector3 position { get; }

    public EnemyMovement movementcontroller { get; }
}

public class Louse : MonoBehaviour, ILouse
{
    [SerializeField]
    private AudioClip m_DeathSFX;
    [SerializeField]
    private HandSummoner m_Summoner;
    [SerializeField]
    private EnemyMovement m_EnemyMovementController;

    public bool isAlive { get => gameObject.activeSelf;}

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public Vector3 position => transform.position;

    public EnemyMovement movementcontroller => m_EnemyMovementController;

    private void Start()
    {
        gameObject.SetActive(false);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScratchZone"))
        {
            gameObject.SetActive(false);

        }
    }

    private void OnDisable()
    {
        if (GameManager.I && !GameManager.I.GameOver)
        {
            UIManager.I.RemoveLice();
            m_Summoner.enabled = false;
            m_EnemyMovementController.OnDied(transform.position);
            if(AudioManager.I)
                AudioManager.I.PlayOneShot(m_DeathSFX);
        }
    }

    private void OnEnable()
    {
        if (GameManager.I && !GameManager.I.GameOver)
        {
            UIManager.I.AddLice();
            m_Summoner.enabled = true;
        }
    }
}
