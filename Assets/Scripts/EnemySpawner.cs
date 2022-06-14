using UnityEngine;
using System.Collections;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawner starting properties")]

    [SerializeField]
    private float timeBetweenWaves;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private int enemiesPerWave, enemyHitpoints, enemyGoldValue;

    [Header("Spawner upgrade properties")]

    [SerializeField]
    private int upgradeAfterNWaves;
    [SerializeField]
    private int enemyIncrease, hitpointIncrease, goldIncrease;
    [SerializeField]
    private float speedIncrease;

    [Header("GUI")]
    public TextMeshProUGUI waveCounter;

    [Header("Enemy Sprites")]
    public RuntimeAnimatorController[] enemySprites;


    private float countdown = 2f;
    private int currentWave = 0;
    public static int enemiesAlive = 0;


    void Update()
    {
        if (countdown <= 0f && enemiesAlive <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

    }

    private IEnumerator SpawnWave()
    {
        currentWave++;
        if (currentWave % upgradeAfterNWaves == 0)
        {
            increaseDifficulty();
        }
        waveCounter.text = $"Wave {currentWave}\n";
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void increaseDifficulty()
    {
        enemiesPerWave += enemyIncrease;
        enemyHitpoints += hitpointIncrease;
        enemyGoldValue += goldIncrease;
        enemySpeed += speedIncrease;
    }

    private void SpawnEnemy()
    {
        enemiesAlive++;
        GameObject temp = Instantiate(enemyPrefab, transform.position, transform.rotation);
        Enemy enemy = temp.GetComponent<Enemy>();

        enemy.setValues(enemySpeed, enemyHitpoints, enemyGoldValue);
        Traits.Trait enemyType = Traits.chooseTrait();
        enemy.setType(enemyType);
        Animator enemyAnimator = enemy.gameObject.GetComponent<Animator>();
        enemyAnimator.runtimeAnimatorController = enemySprites[(int)enemyType];
    }


}
