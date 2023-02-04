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
    private bool m_RunningAway = false;

    void Update()
    {
        if (!m_WanderArea.bounds.Contains(m_Enemy.position) && !m_RunningAway)//Remove !m_RunningAway to stop them from leaving area.
        {
            m_Timer = 0;
            m_Delay = Random.Range(.5f, 1f);
            m_Vel = (new Vector3(m_WanderArea.transform.position.x, m_Enemy.velocity.y, m_WanderArea.transform.position.z) - new Vector3(m_Enemy.position.x, m_Enemy.velocity.y, m_Enemy.position.z)).normalized * m_Speed;
        }

        if (m_Timer >= m_Delay)
        {
            m_RunningAway = false;
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

    public void MoveAwayFrom(Vector3 inPoint)
    {
        m_RunningAway = true;
        m_Vel = -((new Vector3(inPoint.x, m_Enemy.velocity.y, inPoint.z) - new Vector3(m_Enemy.position.x, m_Enemy.velocity.y, m_Enemy.position.z)).normalized) * m_Speed;
        m_Delay = 3f;
        m_Timer = 0;
    }
}
