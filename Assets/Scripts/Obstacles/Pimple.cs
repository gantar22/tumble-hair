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
    [SerializeField] private float m_SpawnVelUp = 10;
    [SerializeField] private float m_AnimationTime = 1f;
    [SerializeField] private float m_MaxHeight = 12f;
    private List<Puss> m_ActivePuss = new List<Puss>();
    private Stack<Puss> m_PussPool = new Stack<Puss>();
    private float m_SpawnTimer = 0f;
    private float m_SpawnDelay = 0f;
    private float m_AnimationTimer = 0f;
    private bool m_Pop = false;
    private bool m_Burst = false;
    private Vector3 m_OGScale = Vector3.zero;
    private Vector3 m_MaxScale = Vector3.zero;

    private void Start()
    {
        m_OGScale = transform.localScale;
        m_SpawnDelay = Random.Range(m_MinSpawnTime,m_MaxSpawnTime);
        m_MaxScale = new Vector3(m_OGScale.x,m_MaxHeight,m_OGScale.z);
    }

    private void Update()
    {
        if(m_Pop)
        {
            UpdatePopAnimation(Time.deltaTime);
        }
        else if (m_Burst)
        {
            UpdateBurstAnimation(Time.deltaTime);
        }
        else
        {
            UpdateSpawnRoutine(Time.deltaTime);
        }
    }

    private void Spawn(Vector3 inDirection, float inSpeed)
    {
        Puss puss = null;
        if(m_PussPool.Count + m_ActivePuss.Count < m_Limit)
        {
            puss = Instantiate(m_PussPrefab);
        }
        else
        {
            puss = m_PussPool.Pop();
        }

        m_ActivePuss.Add(puss);
        puss.OnDeath.AddListener(OnDeath);
        puss.transform.position = m_SpawnPoint.position;
        puss.gameObject.SetActive(true);
        puss.GetComponent<Rigidbody>().velocity = new Vector3(inDirection.x,m_SpawnVelUp, inDirection.z) * inSpeed;
    }

    private void OnDeath(Puss inObject)
    {       
        m_ActivePuss.Remove(inObject);
        m_PussPool.Push(inObject);
    }

    public void UpdateSpawnRoutine(float inDT)
    {
        if (m_SpawnTimer >= m_SpawnDelay)
        {
            if (m_ActivePuss.Count < m_Limit)
            {
                m_Pop = true; //Activate Pop Animation
            }
            else
            {
                m_SpawnTimer = 0;
                m_SpawnDelay = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
            }
        }
        else
        {
            m_SpawnTimer += inDT;
        }
    }

    public void UpdatePopAnimation(float inDT)
    {
        if (m_AnimationTimer >= m_AnimationTime)
        {
            m_AnimationTimer = 0f;
            transform.localScale = m_MaxScale;
            Spawn(new Vector3(Random.Range(-m_SpawnRadius, m_SpawnRadius), 1f, Random.Range(-m_SpawnRadius, m_SpawnRadius)), m_SpawnVel);
            m_Pop = false;
            m_Burst = true;//Activate Burst animation
        }
        else
        {
            transform.localScale = Vector3.Slerp(m_OGScale, m_MaxScale, m_AnimationTimer/m_AnimationTime);
            m_AnimationTimer += inDT;
        }
    }

    public void UpdateBurstAnimation(float inDT)
    {
        if (m_AnimationTimer >= m_AnimationTime)
        {
            m_AnimationTimer = 0f;
            m_Burst = false;
            transform.localScale = m_OGScale;
            m_SpawnTimer = 0;
            m_SpawnDelay = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
        }
        else
        {
            transform.localScale = Vector3.Slerp(m_MaxScale, m_OGScale, m_AnimationTimer / m_AnimationTime);
            m_AnimationTimer += inDT;
        }
    }

}
