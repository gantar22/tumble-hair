using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButton : MonoBehaviour
{
    [SerializeField] GameObject popuptoclose;
    [SerializeField] GameObject popuptoopen;

    public void DisableCurrentScreen()
    {
        popuptoclose.SetActive(false);
    }

    public void EnablePopup()
    {
        popuptoopen.SetActive(true);
    }
}
