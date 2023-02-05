using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("In seconds")]
    private float m_GameTime;
    [SerializeField]
    private Animator m_HUDAnim;
    [SerializeField]
    private DualProgressBar m_Bar;
    [SerializeField]
    private Transform m_WinScreen;
    [SerializeField]
    private Transform m_LoseScreen;
    private bool m_GameOver = false;
    private float m_Timer;

    private void Awake()
    {
        foreach (var chunk in GetComponentsInChildren<Chunk>())
        {
            chunk.Commence();
        }

        m_Timer = m_GameTime;
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void LoadScene(int inSceneID)
    {
        SceneManager.LoadScene(inSceneID);
    }

    private void Update()
    {
        if(!m_GameOver)
        {
            if (m_Timer <= 0)
            {
                m_LoseScreen.gameObject.SetActive(true);
                m_HUDAnim.gameObject.SetActive(false);
                m_GameOver = true;
            }
            m_Timer -= Time.deltaTime;
            m_Bar.SetTime(m_Timer/m_GameTime);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_HUDAnim)
            {
                Pause(Time.timeScale == 0 ? false : true);
            }
            else
            {
                CloseApp();
            }
        }
    }

    public void Pause(bool inValue)
    {
        m_HUDAnim.SetBool("paused", inValue);
        if (inValue)
       {
            Time.timeScale = 0;
       }
       else
       {
            Time.timeScale = 1;
        }
    }
}
