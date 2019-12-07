using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuider : MonoBehaviour
{
    [SerializeField]
    private LevelPart[] m_LevelPart;
    [SerializeField]
    private GameObject m_PlayerPrefab;
    private LevelPart[] m_levelPartsInstantiate;
    private Transform m_Player;
    private CharacterController m_CharacterController;
    private LevelPart m_LastLevelPartSpawn;
    private int count = 0;
    private int m_PartCount = 0;
    private bool m_NextPartSpawn = true;

    [SerializeField]
    private Queue<LevelPart> m_levelPartSpawned = new Queue<LevelPart>();
    private int m_nbPart = 5;
    private float m_DistanceToEndPointBeforeSpawningPart = 30.0f;

    void Awake()
    {
        m_Player = Instantiate(m_PlayerPrefab.transform, m_LevelPart[0].LevelTransform.transform.position, Quaternion.identity);
        m_CharacterController = m_Player.GetComponent<CharacterController>();

        m_levelPartsInstantiate = new LevelPart[m_LevelPart.Length * m_nbPart];
        InstantiateLevelPart();

        SpawnFirstParts();
    }

    private void SpawnFirstParts()
    {
        m_LastLevelPartSpawn = Instantiate(m_LevelPart[0].LevelTransform, m_LevelPart[0].LevelTransform.transform.position, Quaternion.identity).GetComponent<LevelPart>();
        m_levelPartSpawned.Enqueue(m_LastLevelPartSpawn);

        for(int i = 0; i < m_nbPart; i++)
        {
            SpawnLevelPart();
        }
    }

    private void InstantiateLevelPart()
    {
        int k = 0;
        for(int i = 0; i < m_LevelPart.Length; i++)
        {
            for(int j = 0; j < m_nbPart; j++)
            {
                m_levelPartsInstantiate[k] = Instantiate(m_LevelPart[i]).GetComponent<LevelPart>();
                m_levelPartsInstantiate[k].gameObject.SetActive(false);
                k++;
            }
        }
    }

    private LevelPart GetRandomPart()
    {
        int vRandomIndex = UnityEngine.Random.Range(0, m_levelPartsInstantiate.Length - 1);
        LevelPart vPartToReturn = m_levelPartsInstantiate[vRandomIndex];
        if (vPartToReturn.isSpawn)
        {
            vPartToReturn = GetRandomPart();
        }
        return vPartToReturn;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(m_Player.position, m_LastLevelPartSpawn.EndPoint.position) < m_DistanceToEndPointBeforeSpawningPart)
        {
            SpawnLevelPart();
            DeletePart();
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

    private void SpawnLevelPart()
    {
        LevelPart vLevelPartToSpawn = GetRandomPart();

        vLevelPartToSpawn.transform.position = m_LastLevelPartSpawn.EndPoint.position;
        vLevelPartToSpawn.transform.rotation = Quaternion.identity;

        vLevelPartToSpawn.gameObject.SetActive(true);
        vLevelPartToSpawn.isSpawn = true;
        m_levelPartSpawned.Enqueue(vLevelPartToSpawn);
        m_LastLevelPartSpawn = vLevelPartToSpawn;

        m_NextPartSpawn = true;
        m_PartCount += 1;
    }

    private void DeletePart()
    {
        LevelPart vPartToDelete = m_levelPartSpawned.Dequeue();
        vPartToDelete.isSpawn = false;
        vPartToDelete.gameObject.SetActive(false);
    }
}
