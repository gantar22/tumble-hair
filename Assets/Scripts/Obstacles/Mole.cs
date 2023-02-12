using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField] private float m_Bounce = 5f;
    [SerializeField] private float m_MinScale;
    [SerializeField] private float m_AnimTime = .5f;
    private Vector3 m_OGScale;
    private bool m_AnimContract = false;
    private bool m_AnimExpand = false;
    private float m_AnimTimer = 0;

    private void Start()
    {
        m_OGScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<CharacterController>();
            player.Bounce(m_Bounce);
            if(!m_AnimContract && !m_AnimExpand)
            {
                m_AnimTimer = 0;
                m_AnimContract = true;
            }
        }
    }

    private void Update()
    {
        if (m_AnimContract)
        {
            if (m_AnimTimer > m_AnimTime)
            {
                transform.localScale = new Vector3(m_MinScale * m_OGScale.x, m_OGScale.y, m_MinScale * m_OGScale.z);
                m_AnimTimer = 0f;
                m_AnimContract = false;
                m_AnimExpand = true;
            }
            else
            {
                transform.localScale = Vector3.Slerp(m_OGScale, new Vector3(m_MinScale * m_OGScale.x, m_OGScale.y, m_MinScale * m_OGScale.z), m_AnimTimer / m_AnimTime);
                m_AnimTimer += Time.deltaTime;
            }
        }
        else if (m_AnimExpand)
        {
            if (m_AnimTimer > m_AnimTime)
            {
                transform.localScale = m_OGScale;
                m_AnimTimer = 0f;
                m_AnimExpand = false;
            }
            else
            {
                transform.localScale = Vector3.Slerp( new Vector3(m_MinScale * m_OGScale.x, m_OGScale.y, m_MinScale * m_OGScale.z), m_OGScale, m_AnimTimer / m_AnimTime);
                m_AnimTimer += Time.deltaTime;
            }
        }
    }
}
