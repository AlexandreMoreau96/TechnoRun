using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] m_ParticleSystems;
    [SerializeField] private Transform[] m_SpawnPoints;
    [SerializeField] private bool m_RandomSpawnPoints = false;
    [SerializeField] private bool m_RandomSpawnParticles = true;

    private ParticleSystem[] m_ParticleSystemsInstances;
    private Queue<ParticleSystem> m_SpawnedParticleSystems = new Queue<ParticleSystem>();
    private int m_nbParticleSystems = 6;
    private int m_LastParticleToBeSpawned = 0;
    private int m_LastSpawnPoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_ParticleSystemsInstances = new ParticleSystem[m_ParticleSystems.Length * m_nbParticleSystems];
        InstantiateParticles();
    }

    private void InstantiateParticles()
    {
        int k = 0;
        for (int i = 0; i < m_ParticleSystems.Length; i++)
        {
            for (int j = 0; j < m_nbParticleSystems; j++)
            {
                m_ParticleSystemsInstances[k] = Instantiate(m_ParticleSystems[i]);
                m_ParticleSystemsInstances[k].Stop();
                k++;
            }
        }
    }

    public void CreateParticle()
    {

        if (m_SpawnedParticleSystems.Count == m_nbParticleSystems - 1)
        {
            DeleteParticle();
        }

        SpawnParticleSystem();
    }

    private void SpawnParticleSystem()
    {
        ParticleSystem vParticleSystemToSpawn;
        int vLastPartToSpawn = m_LastParticleToBeSpawned;
        int vLastSpawnPoint = m_LastSpawnPoint;

        if(vLastPartToSpawn == m_ParticleSystems.Length - 1)
        {
            vLastPartToSpawn = 0;
        }
        else
        {
            vLastPartToSpawn++;
        }

        if (vLastSpawnPoint == m_SpawnPoints.Length - 1)
        {
            vLastSpawnPoint = 0;
        }
        else
        {
            vLastSpawnPoint++;
        }

        if (m_RandomSpawnParticles)
        {
            vParticleSystemToSpawn = m_ParticleSystemsInstances[Random.Range(0, m_ParticleSystems.Length - 1)];
        }
        else
        {
            vParticleSystemToSpawn = m_ParticleSystemsInstances[vLastPartToSpawn];
        }
        

        vParticleSystemToSpawn.transform.position = m_SpawnPoints[vLastSpawnPoint].position;
        vParticleSystemToSpawn.transform.rotation = Quaternion.identity;

        vParticleSystemToSpawn.Play();
        
        m_SpawnedParticleSystems.Enqueue(vParticleSystemToSpawn);
        m_LastParticleToBeSpawned = vLastPartToSpawn;
        m_LastSpawnPoint = vLastSpawnPoint;
    }

    private void DeleteParticle()
    {
        ParticleSystem vParticleToDelete = m_SpawnedParticleSystems.Dequeue();
        vParticleToDelete.Stop();
    }
}
