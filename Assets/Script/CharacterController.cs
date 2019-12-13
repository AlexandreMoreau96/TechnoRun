using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float m_Speed = 3f;
    private Animator m_AnimationController;
    private float m_Y;
    private float m_JumpHeight = 1.5f;
    private float m_JumpSpeed = 2f;
    private bool m_CanJump = true;
<<<<<<< Updated upstream
=======
    public Vector3 m_Velocity;
    private float m_AnimationSpeedRatio = 6;
    private bool m_Falling = false;
    public bool m_Dead = false;
    [SerializeField] private GameObject m_ParticleR;
    [SerializeField] private GameObject m_ParticleL;
    [SerializeField] private ParticleSystem m_ParticleSystemSteps;
    private ParticleSystem[] m_ParticleSystemInstances;
    private Queue<ParticleSystem> m_ParticleSystemQueue = new Queue<ParticleSystem>();
    private int m_MaxParticleSystems = 6;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        m_AnimationController = GetComponent<Animator>();
        m_AnimationController.SetFloat("speed", m_Speed / 7);
        m_Y = transform.position.y;

        SetParticlePooling();
    }

    private void SetParticlePooling()
    {
        m_ParticleSystemInstances = new ParticleSystem[m_MaxParticleSystems];

        for (int i = 0; i < m_MaxParticleSystems; i++)
        {
            m_ParticleSystemInstances[i] = Instantiate(m_ParticleSystemSteps);
            m_ParticleSystemInstances[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 verMovement = transform.right;
        Vector3 horMovement = -transform.forward * Input.GetAxisRaw("Horizontal");
        Vector3 vel = (horMovement + verMovement).normalized * m_Speed;
        transform.position += vel * Time.deltaTime;

        if (m_CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            m_CanJump = false;
            m_AnimationController.SetTrigger("JumpPressed");
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        while(transform.position.y < m_JumpHeight)
        {
            transform.position += new Vector3(0, m_JumpSpeed, 0) * Time.deltaTime;
            yield return null;
        }

        m_AnimationController.speed = 0;

        while (transform.position.y >= m_Y)
        {
            transform.position += new Vector3(0, -2 * m_JumpSpeed, 0) * Time.deltaTime;
            yield return null;
        }

        m_AnimationController.SetTrigger("TouchedGround");
        m_AnimationController.speed = 1;
        m_CanJump = true;
    }

    internal void IncreaseSpeed()
    {
        m_Speed *= 1.5f;
        m_Speed = m_Speed >= 35.0f ? 35.0f : m_Speed;
        m_AnimationController.SetFloat("speed", m_Speed / 7);
    }

    public void CreateParticles(int foot)
    {
        ParticleSystem vNewParticle;
        if (m_ParticleSystemQueue.Count > m_MaxParticleSystems)
        {
            vNewParticle = m_ParticleSystemQueue.Dequeue();
            vNewParticle.gameObject.SetActive(false);

        }
        else
        {
            vNewParticle = GetParticle();
        }

        if (foot == 0)
        {
            m_ParticleSystemQueue.Enqueue(vNewParticle);
        }
        else 
        {
            
        }
    }

    private ParticleSystem GetParticle()
    {
        throw new NotImplementedException();
    }
}
