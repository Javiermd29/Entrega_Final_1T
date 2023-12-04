using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] coinArray;
    private int coinIndex;

    private float spawnRangeX = 8.60f;
    private float spawnRangeZ = 6.40f;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float spawnInterval = 4f;

    private PlayerController playerControllerScript;

    void Start()
    {

        playerControllerScript = FindObjectOfType<PlayerController>();

        InvokeRepeating("SpawnRandomCoin",
            startDelay,
            spawnInterval);

    }

    private void Update()
    {
        if (playerControllerScript.isGameOver)
        {
            CancelInvoke("SpawnRandomCoin");
        }
    }

    private void SpawnRandomCoin()
    {

        coinIndex = Random.Range(0, coinArray.Length);

        Instantiate(coinArray[coinIndex],
            RandomSpawnPos(),
            Quaternion.identity);

    }

    private Vector3 RandomSpawnPos()
    {

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        return new Vector3(randomX, 0.5f, randomZ);

    }
}
