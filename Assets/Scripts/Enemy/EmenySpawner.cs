using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime, maxSpawnTime;
    [SerializeField] private float acceleration;
    [SerializeField] private Enemy[] enemyList;
    private bool isGameStarted;

    private void Start()
    {
        isGameStarted = false;
    }

    public void OnGameStarted()
    {
        isGameStarted = true;
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            Instantiate(enemyList[Random.Range(0, enemyList.Length)].gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!isGameStarted) return;

        minSpawnTime -= acceleration * Time.fixedDeltaTime;
        maxSpawnTime -= acceleration * Time.fixedDeltaTime;

        minSpawnTime = Mathf.Clamp(minSpawnTime, 1, 20);
        maxSpawnTime = Mathf.Clamp(maxSpawnTime, 1, 20);
    }
}
