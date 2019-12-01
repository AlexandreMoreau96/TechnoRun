using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuider : MonoBehaviour
{
    [SerializeField]
    private LevelPart m_LevelPart;
    [SerializeField]
    private GameObject m_PlayerPrefab;
    private Transform m_Player;
    private CharacterController m_CharacterController;
    private LevelPart m_LastLevelPartSpawn;
    private int count = 0;
    private int m_PartCount = 0;
    private bool m_NextPartSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = Instantiate(m_PlayerPrefab.transform, m_LevelPart.LevelTransform.transform.position, Quaternion.identity);
        m_CharacterController = m_Player.GetComponent<CharacterController>();
        m_LastLevelPartSpawn = Instantiate(m_LevelPart.LevelTransform, m_LevelPart.LevelTransform.transform.position, Quaternion.identity).GetComponent<LevelPart>();
        SpawnLevelPart();
        SpawnLevelPart();
        SpawnLevelPart();
        SpawnLevelPart();
        SpawnLevelPart();
        SpawnLevelPart();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(m_Player.position, m_LastLevelPartSpawn.EndPosition.position) < 50)
        {
            SpawnLevelPart();
        }
        if(m_PartCount % 10 == 0 && m_NextPartSpawn)
        {
            IncreaseSpeed();
            m_NextPartSpawn = false;
        }
    }

    private void IncreaseSpeed()
    {
        m_CharacterController.IncreaseSpeed();
    }

    public void SpawnLevelPart()
    {
        m_LastLevelPartSpawn = Instantiate(m_LevelPart, m_LastLevelPartSpawn.EndPosition.position, Quaternion.identity).GetComponent<LevelPart>();
        m_NextPartSpawn = true;
        m_PartCount += 1;
    }
}
