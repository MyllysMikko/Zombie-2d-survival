using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] GameObject zombiePrefab;
    [SerializeField] List<GameObject> zombies = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] int waveNumber;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] bool ongoingWave;
    [SerializeField] int zombieAmmount;
    [SerializeField] int zombiesKilled;

    [Header("Stat increase")]
    [SerializeField] int wavesForIncrease;
    [SerializeField] int HPIncrease;

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

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartWave();
    }

    void StartWave()
    {
        waveNumber++;
        ongoingWave = true;
        zombieAmmount = 10 * waveNumber;
        zombiesKilled = 0;
        for (int i = 0; i < zombieAmmount; i++)
        {
            SpawnEnemy();
        }
    }

    // Uudelleen käytetään kuolleita zombeja.
    // Jos on saatavilla kuollut zombi (Inactive). Asetetaan tälle uusi hp ja viedään spawni pisteelle
    // Muuten luodaan kokonaan uusi
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

                EnemyHPController enemyHPController = zombie.GetComponent<EnemyHPController>();
                SetZombieStats(enemyHPController);

                foundInactive = true;
                break;
            }
        }

        if (!foundInactive)
        {
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            EnemyHPController enemyHPController = zombie.GetComponent<EnemyHPController>();
            SetZombieStats(enemyHPController);
            enemyHPController.Died += OnZombieDead;
            zombies.Add(zombie);
        }
    }

    void SetZombieStats(EnemyHPController zombie)
    {

        int hp = HPIncrease * (int)(waveNumber / wavesForIncrease);

        zombie.SetHP(hp);
    }

    void OnZombieDead(System.Object sender, EventArgs e)
    {
        zombiesKilled++;
        if (zombiesKilled == zombieAmmount)
        {
            Debug.Log("Wave over!");
            ongoingWave = false;
            StartCoroutine(StartNextWave());
        }
    }



    Vector3 GetSpawnpoint()
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnIndex].position;
        spawnPosition.y += UnityEngine.Random.Range(-3, 3);
        spawnPosition.x += UnityEngine.Random.Range(-3, 3);


        return spawnPosition;
    }
}
