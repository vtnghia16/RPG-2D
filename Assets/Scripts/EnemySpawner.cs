using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Time Intervals")]
    [SerializeField] private float minSpawnInterval = 10.0f;
    [SerializeField] private float maxSpawnInterval = 20.0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            // Chọn một prefab ngẫu nhiên từ mảng enemyPrefabs
            GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Kiểm tra xem prefab đã bị hủy chưa
            if (randomPrefab != null)
            {
                // Tính toán vị trí spawn dựa trên vị trí của người chơi và offset
                Vector3 spawnPosition = new Vector3(player.transform.position.x + 5 + Random.Range(-10, 20) , 16, 0);

                // Spawn enemy sử dụng prefab được chọn và vị trí ngẫu nhiên
                GameObject newEnemy = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Destroy(randomPrefab);
                Debug.LogError("Prefab is null!");
            }
        }
    }
}

