using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float m_Speed = 10f;
    private Animator m_AnimationController;
    private float m_Y;
    private float m_JumpHeight = 1.5f;
    private float m_JumpSpeed = 2f;
    private bool m_CanJump = true;
    public Vector3 m_Velocity;
    private float m_AnimationSpeedRatio = 6;

    // Start is called before the first frame update
    void Start()
    {
        m_AnimationController = GetComponent<Animator>();
        m_AnimationController.SetFloat("speed", m_Speed / m_AnimationSpeedRatio);
        m_Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 verMovement = transform.right;
        Vector3 horMovement = -transform.forward * Input.GetAxisRaw("Horizontal");
        m_Velocity = (horMovement + verMovement).normalized * m_Speed;
        transform.position += m_Velocity * Time.deltaTime;

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
        m_AnimationController.SetFloat("speed", m_Speed / m_AnimationSpeedRatio);
    }
}
