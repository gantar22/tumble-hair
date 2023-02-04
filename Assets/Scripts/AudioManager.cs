using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I = null;

    [SerializeField]
    private AudioSource m_Source;

    private float m_SFXVolume = 1;
    public float SFXVolume => m_SFXVolume;
    public float MusicVolume => m_Source.volume;

    private void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayOneShot(AudioClip inClip)
    {
        m_Source.PlayOneShot(inClip, m_SFXVolume);
    }

    public void SetMusicVolume(float inVol)
    {
        m_Source.volume = inVol;
    }

    public void PauseMusic(bool inPause)
    {
        if(inPause)
        {
            m_Source.Pause();
        }
        else
        {
            m_Source.UnPause();
        }
    }
}
