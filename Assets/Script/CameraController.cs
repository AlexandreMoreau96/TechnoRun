using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CharacterController m_CharacterController;
    private GameManager m_GM;
    private bool m_gameOver = false;

    private void Start()
    {
        m_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if((m_CharacterController.gameObject.transform.position.x - 2.6f <= transform.position.x ||
            m_CharacterController.m_Dead) && !m_gameOver)
        {
            //Time.timeScale = 0.0f;
            m_GM.GameOver();
            m_gameOver = true;
            m_CharacterController.m_Velocity = Vector3.zero;
            m_CharacterController.gameObject.SetActive(false);
        }

        transform.position += m_CharacterController.m_Velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, m_CharacterController.transform.position.z);
    }
}
