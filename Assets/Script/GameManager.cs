using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
    [SerializeField] private AudioSource m_audioSource2;
    [SerializeField] private EasingManager m_easingManager;
    private int m_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_UI.SetActive(false);
        m_audioSource.volume = m_musicSlider.value;
        m_audioSource2.volume = m_musicSlider.value;
        m_AudioMixer.SetFloat("Clip2Volume", -80f);
        m_audioSource.Play();
    }


    public void GameOver()
    {
        m_scoreText.text = "Score : " + (m_score - 5).ToString();
        m_UI.SetActive(true);
        StartCoroutine(PlayGameOverSounds());
        StartCoroutine(m_easingManager.Test2());
        StartCoroutine(m_easingManager.Test());
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private IEnumerator PlayGameOverSounds()
    {
        m_AudioMixer.SetFloat("Clip1Volume", 20f);

        m_audioSource2.Stop();
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
        m_AudioMixer.SetFloat("Clip2Volume", -20f);

        StartCoroutine(FadedClip2());
        StartCoroutine(FadedClip());
    }

    public void SetVolumeSFX(float value)
    {
        m_AudioMixer.SetFloat("SFXVolume", value);
    }

    public void SetVolumeMusic(float value)
    {
        m_audioSource.volume = value;
        m_audioSource2.volume = value;
    }

    public void SetFoleyVolume(float value)
    {
        m_AudioMixer.SetFloat("FoleyVolume", value);
    }

    public IEnumerator FadedClip2()
    {
        m_AudioMixer.GetFloat("Clip2Volume", out float value2);
        while (value2 <= 20f)
        {
            m_AudioMixer.GetFloat("Clip2Volume", out value2);
            m_AudioMixer.SetFloat("Clip2Volume", value2 + 0.001f);
            yield return null;
        }
        m_AudioMixer.SetFloat("Clip2Volume", 20f);

    }

    public IEnumerator FadedClip()
    {
        m_AudioMixer.GetFloat("Clip1Volume", out float value);
        while ( value >= -10f)
        {
            m_AudioMixer.GetFloat("Clip1Volume", out value);
            m_AudioMixer.SetFloat("Clip1Volume", value - 0.0005f);
            yield return null;
        }
        m_AudioMixer.SetFloat("Clip1Volume", -80f);

    }
}
