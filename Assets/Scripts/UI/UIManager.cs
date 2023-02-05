using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager I;
    [SerializeField]
    private Transform m_TutorialScreen;
    [SerializeField]
    private Animator m_HUDAnim;
    [SerializeField]
    private DualProgressBar m_Bar;
    [SerializeField]
    private Transform m_WinScreen;
    [SerializeField]
    private Transform m_LoseScreen;
    [SerializeField]
    private int m_CurrSceneID;
    [SerializeField]
    private int m_NextSceneID;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        if(m_TutorialScreen.gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            StartGame();
        }
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void LoadScene(int inSceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(inSceneID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.I.GameOver)
        {
            if(m_TutorialScreen.gameObject.activeSelf)
            {
                m_TutorialScreen.gameObject.SetActive(false);
            }
            else
            {
                if (m_HUDAnim)
                {
                    Pause(Time.timeScale == 0 ? false : true);
                }
                else
                {
                    CloseApp();
                }
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

    public void SetTime(float inPercent)
    {
        m_Bar.SetTime(inPercent);
    }

    public void SetHair(float inPercent)
    {
        m_Bar.SetHair(inPercent);
    }

    public void GameOver(bool inWon)
    {
        if (inWon)
        {
            m_WinScreen.gameObject.SetActive(true);
            m_HUDAnim.gameObject.SetActive(false);
        }
        else
        {
            m_LoseScreen.gameObject.SetActive(true);
            m_HUDAnim.gameObject.SetActive(false);
        }
    }

    public void LoadNextScene()
    {
        LoadScene(m_NextSceneID);
    }

    public void ReloadScene()
    {
        LoadScene(m_CurrSceneID);
    }

    public void StartGame()
    {
        GameManager.I.Commence();
    }
}
