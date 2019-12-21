using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] m_ParticleSystems;
    private ParticleSystem[][] m_ParticleSystemsInstances;
    
    private Queue<ParticleSystem>[] m_SpawnedParticleSystems;
    [SerializeField] private int[] m_MaxParticles;
    private int[] m_LastPart;

    private void Awake()
    {
        m_ParticleSystemsInstances = new ParticleSystem[m_ParticleSystems.Length][];
        m_SpawnedParticleSystems = new Queue<ParticleSystem>[m_ParticleSystems.Length];
        m_LastPart = new int[m_ParticleSystems.Length];

        for (int i = 0; i < m_ParticleSystems.Length; i++)
        {
            m_ParticleSystemsInstances[i] = new ParticleSystem[m_MaxParticles[i]];
            for (int j = 0; j < m_MaxParticles[i]; j++)
            {
                m_ParticleSystemsInstances[i][j] = Instantiate(m_ParticleSystems[i]);
                m_ParticleSystemsInstances[i][j].Stop();
            }
        }

        for (int i = 0; i < m_ParticleSystems.Length; i++)
        {
            m_SpawnedParticleSystems[i] = new Queue<ParticleSystem>();
        }
    }

    public void CreateParticle(ParticleSystem pSystem, Transform pPos)
    {
        if(m_ParticleSystemsInstances == null)
        {
            return;
        }

        int i = 0;
        foreach(ParticleSystem sys in m_ParticleSystems)
        {
            if(sys.tag == pSystem.tag)
            {
                break;
            }
            i++;
        }


        if (m_SpawnedParticleSystems[i].Count == m_MaxParticles[i] - 1)
        {
            DeleteParticle(i);
        }

        SpawnParticleSystem(i, pPos);
    }

    private void SpawnParticleSystem(int pI, Transform pPos)
    {
        if(m_LastPart[pI] == m_MaxParticles[pI] - 1)
        {
            m_LastPart[pI] = 0;
        }
        else
        {
            m_LastPart[pI]++;
        }

        
        ParticleSystem vParticleSystemToSpawn = m_ParticleSystemsInstances[pI][m_LastPart[pI]];

        vParticleSystemToSpawn.transform.position = pPos.position;
        vParticleSystemToSpawn.transform.rotation = Quaternion.identity;

        vParticleSystemToSpawn.Play();
        
        m_SpawnedParticleSystems[pI].Enqueue(vParticleSystemToSpawn);
    }

    private void DeleteParticle(int pI)
    {
        ParticleSystem vParticleToDelete = m_SpawnedParticleSystems[pI].Dequeue();
        vParticleToDelete.Stop();
    }
}
