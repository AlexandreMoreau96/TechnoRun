using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject m_UI;
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_gameOverClip;

    // Start is called before the first frame update
    void Start()
    {
        m_UI = GameObject.Find("UI");
        m_audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        m_UI.SetActive(false);
        m_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_gameOverClip;
        m_audioSource.Play();
        m_UI.SetActive(true);
    }
}
