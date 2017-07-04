using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource footStepSplash;
    public AudioSource typeWriter;
    public AudioSource button;
    public AudioSource backGroundMusic;

    private static AudioManager m_Instance;

    public static AudioManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    public void BackGroundMusicVolumeControl(float value)
    {
        backGroundMusic.volume = value;
    }

    public void SFXVolumeControl(float value)
    {
        footStepSplash.volume = value;
        typeWriter.volume = value;
    }

    public void FootStepSplashAudio()
    {
        if (!footStepSplash.isPlaying)
        {
            footStepSplash.Play();
        }
    }

    public void TypeWriterAudio()
    {
        typeWriter.Play();
    }

    public void StopTypeWriter()
    {
        typeWriter.Stop();
    }
    
    public void ButtonAudio()
    {
        button.Play();
    }
}
