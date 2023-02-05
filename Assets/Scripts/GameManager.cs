using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField, Tooltip("In seconds")]
    private float m_GameTime;
    private bool m_GameOver = true;
    public bool GameOver => m_GameOver;
    private float m_Timer;
    private Chunk[] m_Chunks = default;

    private void Awake()
    {
        m_Chunks = GetComponentsInChildren<Chunk>();
        if (I == null)
        {
            I = this;
        }
    }

    public void Commence()
    {
        if (m_GameOver)
        {
            UIManager.I.Pause(false);

            foreach (var chunk in m_Chunks)
            {
                chunk.Commence();
            }

            m_Timer = 0;
            m_GameOver = false;
        }
    }

    private void Update()
    {
        if (!m_GameOver)
        {
            if (m_Timer >= m_GameTime)
            {
                UIManager.I.GameOver(false);
                m_GameOver = true;
            }
            m_Timer += Time.deltaTime;
            UIManager.I.SetTime(m_Timer / m_GameTime);
        }
    }
}
