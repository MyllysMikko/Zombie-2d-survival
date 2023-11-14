using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int waveNumber;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] bool ongoingWave;
    [SerializeField] int zombieAmmount;
    [SerializeField] int zombiesKilled;

    [Header("Stat increase")]
    [SerializeField] int wavesForIncrease;
    [SerializeField] int baseHP;
    [SerializeField] int HPIncrease;
    [SerializeField] int baseDamage;
    [SerializeField] int damageIncrease;

    [Header("")]
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] List<GameObject> zombies = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;



    

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

    // Uudelleen k�ytet��n kuolleita zombeja.
    // Jos on saatavilla kuollut zombi (Inactive). Asetetaan t�lle uusi hp ja vied��n spawni pisteelle
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
                EnemyController enemyController = zombie.GetComponent<EnemyController>();
                SetZombieStats(enemyController);

                foundInactive = true;
                break;
            }
        }

        if (!foundInactive)
        {
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            EnemyController enemyController = zombie.GetComponent<EnemyController>();
            SetZombieStats(enemyController);
            enemyController.Died += OnZombieDead;
            zombies.Add(zombie);
            
        }
    }

    void SetZombieStats(EnemyController zombie)
    {
        int multiplier = waveNumber / wavesForIncrease;

        int hp = baseHP + HPIncrease * multiplier;
        int attackDamage = baseDamage + damageIncrease * multiplier;

        zombie.SetStats(hp, attackDamage);
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