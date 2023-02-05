using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (m_Timer >= m_GameTime + .5f)
            {
                UIManager.I.GameOver(false);
                m_GameOver = true;
            }

            var fillQuantity = m_Chunks.Sum(_ => _.HairFill()) / m_Chunks.Length;
            if (fillQuantity > .95f)
            {
                Invoke("WinGame",.75f);
                m_GameOver = true;
            }
            m_Timer += Time.deltaTime;
            UIManager.I.SetHair(fillQuantity);
            UIManager.I.SetTime(m_Timer / m_GameTime);
        }
    }

    void WinGame()
    {
        UIManager.I.GameOver(true);
    }
}
