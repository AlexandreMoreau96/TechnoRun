using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CharacterController m_CharacterController;
    private GameObject m_UI;

    private void Start()
    {
        m_UI = GameObject.Find("UI");
        m_UI.SetActive(false);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CharacterController.gameObject.transform.position.x <= transform.position.x)
        {
            Time.timeScale = 0.0f;
            m_UI.gameObject.SetActive(true);
        }

        transform.position += m_CharacterController.m_Velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, m_CharacterController.transform.position.z);
    }
}
