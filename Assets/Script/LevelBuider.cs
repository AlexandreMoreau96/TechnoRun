using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuider : MonoBehaviour
{
    [SerializeField]
    private LevelPart m_LevelPart;
    private LevelPart m_LastLevelPartSpawn;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_LastLevelPartSpawn = Instantiate(m_LevelPart.LevelTransform, m_LevelPart.LevelTransform.transform.position, Quaternion.identity).GetComponent<LevelPart>();
        SpawnLevelPart();
    }

    // Update is called once per frame
    void Update()
    {
        if(count % 120 == 0)
        {
            SpawnLevelPart();
        }
        count += 1;
    }

    public void SpawnLevelPart()
    {
        m_LastLevelPartSpawn = Instantiate(m_LevelPart, m_LastLevelPartSpawn.EndPosition.position, Quaternion.identity).GetComponent<LevelPart>();
    }
}
