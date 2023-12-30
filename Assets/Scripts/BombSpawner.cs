using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bomb;

    private Coroutine _spawnBombCoroutine;

    private void Start()
    {
        GameController.Instance.OnStageChange += state =>
        {
            if (state == GameController.GameState.run) StartSpawnBomb();
            if (state == GameController.GameState.over) StopSpawnBomb();
        };
    }

    private void StartSpawnBomb()
    {
        _spawnBombCoroutine = StartCoroutine(SpawnBombCoroutine());
    }

    private void StopSpawnBomb()
    {
        StopCoroutine(_spawnBombCoroutine);
    }

    private IEnumerator SpawnBombCoroutine()
    {
        while (true)
        {
            float secondsForCharge = Random.Range(1.5f, 4);
            yield return new WaitForSeconds(secondsForCharge);

            Vector2 spawnPos = new Vector2(Random.Range(-1.5f, 1.5f), transform.position.y);
            Instantiate(_bomb, spawnPos, Quaternion.identity);
        }
    }
}
