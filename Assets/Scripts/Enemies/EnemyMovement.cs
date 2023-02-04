using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Collider m_WanderArea;
    [SerializeField]
    private Rigidbody m_Enemy;
    [SerializeField]
    private float m_Speed;
    private Vector3 m_Vel;
    private float m_Delay = 0f;
    private float m_Timer = 0f;

    void Update()
    {
        if(!m_WanderArea.bounds.Contains(m_Enemy.position))
        {
            m_Timer = 0;
            m_Delay = Random.Range(.5f,1f);
            m_Vel = (new Vector3(m_WanderArea.transform.position.x,0, m_WanderArea.transform.position.z) - new Vector3(m_Enemy.position.x, m_Enemy.velocity.y, m_Enemy.position.z)).normalized * m_Speed;
        }

        if(m_Timer >= m_Delay)
        {
            m_Timer = 0;
            m_Delay = Random.Range(.5f, 2f);
            m_Vel = new Vector3(Random.Range(-1f, 1f) * m_Speed, m_Enemy.velocity.y, Random.Range(-1f, 1f) * m_Speed);
        }
        else
        {
            m_Timer += Time.deltaTime;
        }

        m_Enemy.velocity = m_Vel;
    }
}
