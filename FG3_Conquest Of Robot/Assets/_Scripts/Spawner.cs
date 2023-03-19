using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public event System.Action OnFireAllEnemy;

    public List<Wave> waves = new List<Wave>();
    
    public Transform enemySpawnPos;

    int currentWaveIndex;

    public float timeBetweenSpawns;
    float nextSpawnTime;

    [SerializeField]
    int enemyAlive;

    [SerializeField]
    bool isAllEnemyDie = false;

    private void Start()
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        if(enemy != null)
        {
            enemy.OnDeath += OneEnemyDeath;
        }
    }

    private void Update()
    {
        if (this.waves.Count <= 0 && this.enemyAlive <= 0)
        {
            if (this.isAllEnemyDie) return;
            this.AllEnemyDie();
            this.isAllEnemyDie = true;
            return;
        }

        if (this.waves.Count <= 0) return;
        if(Time.time > this.nextSpawnTime)
        {
            this.currentWaveIndex = Random.Range(0, this.waves.Count);
            this.nextSpawnTime = Time.time + this.timeBetweenSpawns;
            Enemy spawnedEnemy = Instantiate(this.waves[this.currentWaveIndex].enemy, 
                this.enemySpawnPos.position, 
                Quaternion.identity) as Enemy;
            this.enemyAlive++;
            this.waves[this.currentWaveIndex].enemyCount--;
            if (this.waves[this.currentWaveIndex].enemyCount <= 0)
            {
                this.waves.RemoveAt(this.currentWaveIndex);
            }
            
            //spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    public void OneEnemyDeath()
    {
        this.enemyAlive--;
    }

    protected void AllEnemyDie()
    {
        if (this.OnFireAllEnemy != null)
        {
            this.OnFireAllEnemy();
        }
    }

    /*void OnEnemyDeath()
    {
        this.enemiesRemainingAlive--;
        if(this.enemiesRemainingAlive <= 0)
        {
            this.NextWave();
        }
    }*/

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public Enemy enemy;
    }
}
