using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab of the coin object
    public GameObject agentPrefab; // Prefab of the coin object
    public float spawnInterval = 10f; // Interval between each spawn
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Size of the area where coins can spawn

    private float spawnTimer = 0f;

    private GameObject spawnedCoin = null;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        if (spawnedCoin == null || !spawnedCoin.gameObject.IsDestroyed())
            return;

        Vector3 spawnPosition = new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                                                                agentPrefab.transform.position.y + 2,
                                                                agentPrefab.transform.position.z);

        GameObject obj = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        spawnedCoin = obj;
    }
}
