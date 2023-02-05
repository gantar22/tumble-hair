using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveLoader : MonoBehaviour
{
    public void LoadScene(int inSceneID)
    {
        SceneManager.LoadScene(inSceneID);
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
