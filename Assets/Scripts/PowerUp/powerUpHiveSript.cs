using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpHiveSript : MonoBehaviour
{
    [SerializeField] private GameObject[] _powerUpPrefab;
    
    private bool _playerAlive = true;

    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp()
    {
        while (_playerAlive)
        {
            Instantiate(_powerUpPrefab[Random.Range(0, 3)], new Vector3(Random.Range(-8f, 8f), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2f, 12f));
        }
    }

    public void OnPlayerDeath()
    {
        _playerAlive = false;
    }
}
