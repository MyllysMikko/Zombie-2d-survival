using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deadScoreText;

    [SerializeField] PlayerController player;
    [SerializeField] GameObject deadScreen;
    [SerializeField] GameObject hud;
    [SerializeField] EnemyAudioManager enemyAudio;

    [SerializeField] Score score;

    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject timer;
    TextMeshProUGUI timerText;

    [SerializeField] int waveNumber;
    [SerializeField] float timeBetweenWaves;
    public static bool ongoingWave;
    [SerializeField] int zombieAmmount;
    [SerializeField] int zombiesKilled;

    [Header("Zombie")]
    [SerializeField] int baseZombieAmmount;
    [SerializeField] int zombiesPerWave;
    [SerializeField] float movementSpeed;
    [SerializeField] float speedIncrease;
    [SerializeField] int wavesForIncrease;
    [SerializeField] int baseHP;
    [SerializeField] float HPIncrease;
    [SerializeField] int baseDamage;
    [SerializeField] int damageIncrease;

    [Header("")]
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] List<GameObject> zombies = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;

    int layerOrder = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        player.playerDied += OnPlayerDead;

        deadScreen.SetActive(false);
        timerText = timer.GetComponent<TextMeshProUGUI>();
        timer.SetActive(false);
        UpdateWaveText();

        StartCoroutine(StartNextWave());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    StartCoroutine(StartNextWave());
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }


    }

    IEnumerator StartNextWave()
    {
        timer.SetActive(true);

        float time = timeBetweenWaves;

        while (time > 0)
        {
            time -= Time.deltaTime;
            timerText.text = $"Next wave in {time:0.0}";
            yield return null;
        }
        timer.SetActive(false);
        StartWave();
    }

    void StartWave()
    {
        waveNumber++;
        ongoingWave = true;
        zombieAmmount = baseZombieAmmount + zombiesPerWave * waveNumber;
        zombiesKilled = 0;
        for (int i = 0; i < zombieAmmount; i++)
        {
            SpawnEnemy();
        }

        UpdateWaveText();
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
            enemyController.SetLayerOrder(layerOrder);
            layerOrder++;

            enemyController.Died += OnZombieDead;
            enemyController.Died += enemyAudio.OnDeath;
            enemyController.EnemyHurt += enemyAudio.OnHurt;
            enemyController.EnemyAttack += enemyAudio.OnAttack;
            zombie.SetActive(true);
            zombies.Add(zombie);
            
        }
    }

    void SetZombieStats(EnemyController zombie)
    {
        int multiplier = waveNumber / wavesForIncrease;

        //int hp = baseHP + HPIncrease * multiplier;
        int hp = (int)(baseHP + baseHP * HPIncrease * waveNumber);
        int attackDamage = baseDamage + (damageIncrease * waveNumber);

        float speed = movementSpeed + (speedIncrease * waveNumber);

        zombie.SetStats(hp, speed, attackDamage);
    }

    void OnZombieDead(System.Object sender, EventArgs e)
    {
        zombiesKilled++;
        score.AddScore(10);
        if (zombiesKilled == zombieAmmount)
        {
            score.AddScore(100);
            Debug.Log("Wave over!");
            ongoingWave = false;

            StartCoroutine(StartNextWave());
        }
    }

    void OnPlayerDead(System.Object sender, EventArgs e)
    {
        //TODO: jos j�� aikaa teh�, niin tallenna t�ss� score.
        deadScreen.SetActive(true);
        hud.SetActive(false);
        deadScoreText.text = score.score.ToString();
        Debug.Log(score.score.ToString());

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateWaveText()
    {
        waveText.text = $"Wave {waveNumber}";
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
