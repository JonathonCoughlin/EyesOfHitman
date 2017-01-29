using UnityEngine;
using System.Collections;

public class ClownSpawner : MonoBehaviour {

    public GameObject m_Clown;
    public float m_SpawnInterval;
    public int m_SpawnPoolSize;
    public float m_PoolDelay;

    //States
    private float m_spawnClock = 0f;
    private float m_poolClock = 0f;
    private int m_currentSpawnPool = 0;
    public bool m_Paused = false;
    public bool m_PoolAndSpawnOnCmd = false;
    public enum SpawnState { Paused, Pooling, Spawning, AwaitingCmd };
    public SpawnState m_SpawnState;
    
	// Use this for initialization
	void Start () {
        m_spawnClock = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (m_Paused)
        {
            m_SpawnState = SpawnState.Paused;
        }
        switch (m_SpawnState)
        {
            case SpawnState.Pooling:
                {
                    m_poolClock += Time.deltaTime;
                    if (m_poolClock >= m_PoolDelay)
                    {
                        //Switch to Spawning, Reset Counters
                        m_SpawnState = SpawnState.Spawning;
                        m_poolClock = 0f;
                    }
                    break;
                }
            case SpawnState.Spawning:
                {
                    m_spawnClock += Time.deltaTime;
                    if (m_spawnClock >= m_SpawnInterval)
                    {
                        m_spawnClock = 0f;
                        GameObject addClown = (GameObject)Instantiate(m_Clown);
                        m_currentSpawnPool++;
                    }
                    if (m_currentSpawnPool >= m_SpawnPoolSize)
                    {
                        //Switch to pooling, reset spawn counters
                        if (m_PoolAndSpawnOnCmd)
                        {
                            m_SpawnState = SpawnState.AwaitingCmd;
                        } else
                        {
                            m_SpawnState = SpawnState.Pooling;
                        }                        
                        m_spawnClock = 0f;
                        m_currentSpawnPool = 0;
                    }
                    break;
                }
        }
	}

    public void PoolAndSpawn(float poolTime, int spawnSize, float spawnTime)
    {
        m_poolClock = 0f;
        m_spawnClock = 0f;
        m_currentSpawnPool = 0;
        m_SpawnInterval = spawnTime;
        m_SpawnPoolSize = spawnSize;
        m_PoolDelay = poolTime;
        m_SpawnState = SpawnState.Pooling;
}
}
