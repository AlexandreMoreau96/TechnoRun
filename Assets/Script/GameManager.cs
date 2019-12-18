using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip m_gameOverClip;
    [SerializeField] private AudioClip m_gameOverMusicClip;
    [SerializeField] private AudioClip m_advancedInLevelMusic;
    [SerializeField] private Text m_scoreText;
    [SerializeField] private GameObject m_UI;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private Slider m_musicSlider;
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_soundEffectsSlider;
    [SerializeField] private Slider m_SFXSlider;
    private int m_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_UI.SetActive(false);
        m_audioSource.Play();
    }


    public void GameOver()
    {
        m_scoreText.text = "Score : " + m_score.ToString();
        m_UI.SetActive(true);
        StartCoroutine(PlayGameOverSounds());
    }

    private IEnumerator PlayGameOverSounds()
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_gameOverMusicClip;
        m_audioSource.loop = false;
        m_audioSource.Play();
        while (m_audioSource.isPlaying)
        {
            yield return null;
        }
        m_audioSource.clip = m_gameOverClip;
        m_audioSource.Play();
    }

    public void SetScore(int score)
    {
        m_score = score;
    }

    public void ChangeClip()
    {
        //faire faded ici
        m_audioSource.Stop();
        m_audioSource.clip = m_advancedInLevelMusic;
        m_audioSource.Play();
    }

    public void SetVolumeSFX(float value)
    {
        m_AudioMixer.SetFloat("SFXVolume", value);
    }

    public void SetVolumeMusic(float value)
    {
        m_audioSource.volume = value;
    }

    public void SetFoleyVolume(float value)
    {
        m_AudioMixer.SetFloat("FoleyVolume", value);
    }
}
