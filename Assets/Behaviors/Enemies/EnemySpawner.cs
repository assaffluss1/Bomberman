using System.Collections.Generic;
using UnityEngine;

namespace Behaviors.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemiesToSpawn = 6;

        void Start()
        {
            SpawnEnemies();
        }
        private void SpawnEnemies()
        {
            List<Vector2> validSpawnPoints = new List<Vector2>
            {
                new Vector2(-3.52f, -1.6f), 
                new Vector2(-2.24f, 0.96f),
                new Vector2(-2.56f, -0.64f),
                new Vector2(-0.64f, -0.64f),
                new Vector2(-0.64f, -1.6f),
                new Vector2(4.48f, 0.32f),
                new Vector2(0.64f, -0.64f),
                new Vector2(0f, -1.28f),
                new Vector2(1.28f, 1.28f),
                new Vector2(3.2f, 0.96f),
                new Vector2(3.84f, -1.6f)
            };
            Shuffle(validSpawnPoints);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (i < validSpawnPoints.Count)
                {
                    Instantiate(enemyPrefab, validSpawnPoints[i], Quaternion.identity);
                }
            }
        }
        // Fisher-Yates shuffle algorithm
        private void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}
