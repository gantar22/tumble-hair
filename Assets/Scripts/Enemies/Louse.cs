using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public interface ILouse
{
    public bool isAlive { get; }

    public void Spawn(Vector3 position);
    public Vector3 position { get; }

    public EnemyMovement movementcontroller { get; }
}

public class Louse : MonoBehaviour, ILouse
{
    [SerializeField] private float m_AnimSpeedMultiplier = 1;
    [SerializeField]
    private AudioClip[] m_DeathSFX;

    [SerializeField] private AudioClip m_SpawnSFX = default;
    [SerializeField]
    private HandSummoner m_Summoner;
    [SerializeField]
    private EnemyMovement m_EnemyMovementController;

    [SerializeField] private Animator m_Animator = default;
    [SerializeField] private ParticleSystem m_DeathParticles = default;

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
            m_DeathParticles.Play();
            UIManager.I.RemoveLice();
            m_Summoner.enabled = false;
            m_EnemyMovementController.OnDied(transform.position);
            if(AudioManager.I)
                AudioManager.I.PlayOneShot(m_DeathSFX[(int)(UnityEngine.Random.value * m_DeathSFX.Length) % m_DeathSFX.Length]);
        }
    }

    private void OnEnable()
    {
        if (GameManager.I && !GameManager.I.GameOver)
        {
            if (AudioManager.I)
            {
                AudioManager.I.PlayOneShot(m_SpawnSFX);
            }
            UIManager.I.AddLice();
            m_Summoner.enabled = true;
        }
    }

    private void Update()
    {
        m_Animator.SetBool("IsMoving",m_EnemyMovementController.enemy.velocity.magnitude > .2f);
        m_Animator.SetFloat("speed",m_EnemyMovementController.enemy.velocity.magnitude * m_AnimSpeedMultiplier);
    }
}
