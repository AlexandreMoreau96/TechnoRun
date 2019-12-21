using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_soundWalkingClip;
    private ParticlePooling m_ParticlePooling;

    private void Awake()
    {
        m_ParticlePooling = GetComponent<ParticlePooling>();
    }

    public void PlayStepAudio()
    {
        m_audioSource.clip = m_soundWalkingClip;
        m_audioSource.Play();
    }

    public void CreateParticles()
    {
        m_ParticlePooling.CreateParticle();
    }
}
