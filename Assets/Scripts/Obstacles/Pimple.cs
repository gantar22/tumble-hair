using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pimple : MonoBehaviour
{
    [SerializeField] Puss m_PussPrefab;
    [SerializeField] private float m_MinSpawnTime;
    [SerializeField] private float m_MaxSpawnTime;
    [SerializeField] private float m_SpawnRadius;
    [SerializeField] private int m_Limit;
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private float m_SpawnVel;
    private List<Puss> m_ActivePuss = new List<Puss>();
    private Stack<Puss> m_PussPool = new Stack<Puss>();
    private float m_Timer = 0f;
    private float m_Delay = 0f;

    private void Start()
    {
        m_Delay = Random.Range(m_MinSpawnTime,m_MaxSpawnTime);
    }

    private void Update()
    {
        if(m_Timer >= m_Delay)
        {
            m_Timer = 0;
            m_Delay = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
            if(m_ActivePuss.Count < m_Limit)
            {
                var dir = new Vector3(Random.Range(-m_SpawnRadius, m_SpawnRadius), 1f, Random.Range(-m_SpawnRadius, m_SpawnRadius));
                Spawn(dir, m_SpawnVel);
            }
        }
        else
        {
            m_Timer += Time.deltaTime;
        }
    }

    private void Spawn(Vector3 inDirection, float inSpeed)
    {
        Puss puss = null;
        if(m_PussPool.Count + m_ActivePuss.Count < m_Limit)
        {
            puss = Instantiate(m_PussPrefab, transform);
        }
        else
        {
            puss = m_PussPool.Pop();
        }

        m_ActivePuss.Add(puss);
        puss.OnDeath.AddListener(OnDeath);
        puss.transform.position = m_SpawnPoint.position;
        puss.gameObject.SetActive(true);
        puss.GetComponent<Rigidbody>().velocity = inDirection * inSpeed;
    }

    private void OnDeath(Puss inObject)
    {       
        m_ActivePuss.Remove(inObject);
        m_PussPool.Push(inObject);
    }
}
