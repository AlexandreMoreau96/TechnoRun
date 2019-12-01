using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float m_Speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 verMovement = transform.right;
        Vector3 horMovement = -transform.forward * Input.GetAxisRaw("Horizontal");
        Vector3 vel = (horMovement + verMovement).normalized * m_Speed;
        transform.position += vel * Time.deltaTime;
    }

    internal void IncreaseSpeed()
    {
        m_Speed *= 1.5f;
        m_Speed = m_Speed >= 35.0f ? 35.0f : m_Speed;
    }
}
