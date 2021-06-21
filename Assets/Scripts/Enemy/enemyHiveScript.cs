using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHiveScript : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private float _spawnEnemyTime = 1.5f;

    private bool _playerAlive = true;

    void Start()
    {
        StartCoroutine(SpawnEnemy(_spawnEnemyTime));
    }

    IEnumerator SpawnEnemy(float spawnTime)
    {
        while (_playerAlive == true)
        {
            Vector3 enemySpawnPos = new Vector3(Random.Range(-10.63f, 10.9f), 15f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator SpawnAsteroid(float spawnTime)
    {
        while (_playerAlive == true)
        {
            yield return new WaitForSeconds(spawnTime);
        }
    }
    

    public void OnPlayerDeath()
    {
        _playerAlive = false;
    }
}
