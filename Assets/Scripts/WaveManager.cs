using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] int waveNumber;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] List<GameObject> zombies = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] bool ongoingWave;
    [SerializeField] int zombieAmmount;
    [SerializeField] int zombiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartWave();
        }
    }

    void StartWave()
    {
        zombieAmmount = 10 * waveNumber;
        zombiesKilled = 0;
        for (int i = 0; i < zombieAmmount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        bool foundInactive = false;

        Vector3 spawnPosition = GetSpawnpoint();

        foreach(GameObject zombie in zombies)
        {
            if (zombie.activeInHierarchy == false)
            {
                zombie.SetActive(true);
                zombie.transform.position = spawnPosition;

                //TODO aseta hp!
                EnemyHPController enemyHPController = zombie.GetComponent<EnemyHPController>();
                enemyHPController.SetHP(10);

                foundInactive = true;
                break;
            }
        }

        if (!foundInactive)
        {
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            EnemyHPController enemyHPController = zombie.GetComponent<EnemyHPController>();
            enemyHPController.SetHP(10);
            zombies.Add(zombie);
        }
    }

    Vector3 GetSpawnpoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnIndex].position;
        spawnPosition.y += Random.Range(-3, 3);
        spawnPosition.x += Random.Range(-3, 3);


        return spawnPosition;
    }
}
