using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPref;
    [SerializeField] private int _maxRespawnTime;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(1, _maxRespawnTime));

        var enemy = Instantiate(_enemyPref);
        enemy.transform.position = transform.position;

        StartCoroutine(SpawnEnemy());
    }
}