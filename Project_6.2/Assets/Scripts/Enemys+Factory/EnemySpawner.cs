using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyProbability> enemyFactoriesWithProbs = new List<EnemyProbability>();
    List<EnemyFactory> enemyFactories = new List<EnemyFactory>();

    EnemyFactory enemyFactory;
    public Transform player;

    private void Start() 
    {
        float probabilitySum = 0;

        foreach (var enemy in enemyFactoriesWithProbs)
            probabilitySum += enemy.probability;

        foreach (var enemy in enemyFactoriesWithProbs)
            enemy.probability = Mathf.Floor((enemy.probability / probabilitySum) * 100);

        foreach (var enemy in enemyFactoriesWithProbs)
            for (int i = 0; i < enemy.probability; i++)
                enemyFactories.Add(enemy.factory);
    }

    public void spawnRandomEnemy()
    {
        enemyFactory = enemyFactories[Random.Range(0, enemyFactories.Count)];
        IEnemy enemy = enemyFactory.getEnemy();


        Vector3 direction = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y);
        direction = direction.normalized * Random.Range(3, 50);
        Vector3 position = transform.position + direction;
        
        enemy.positionAndRotation(position, Quaternion.identity);

        
        enemy.Player = player;

        Health enemyHP = enemy.EnemyHP;
        enemyHP.spawnOnDeath.AddListener(transform.GetComponent<ItemSpawner>().spawnRandomItem);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            spawnRandomEnemy();
    }
}