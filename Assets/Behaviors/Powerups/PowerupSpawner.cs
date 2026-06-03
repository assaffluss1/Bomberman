using System.Collections.Generic;
using UnityEngine;

namespace Behaviors.Powerups
{
    public class PowerupSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject blastUpPrefab; 
        [SerializeField] private GameObject bombUpPrefab; 
        [SerializeField] private GameObject invincibilityPrefab; 
        [SerializeField] private GameObject speedUpPrefab;
        [SerializeField] private GameObject chickenBombColaPrefab;

        void Start()
        {
            SpawnPowerups();
        }
        private void SpawnPowerups()
        {
            List<GameObject> itemsToSpawn = new List<GameObject>();
            AddItems(itemsToSpawn, blastUpPrefab, 3);
            AddItems(itemsToSpawn, bombUpPrefab, 2);
            AddItems(itemsToSpawn, invincibilityPrefab, 1);
            AddItems(itemsToSpawn, speedUpPrefab, 2);
            AddItems(itemsToSpawn, chickenBombColaPrefab, 1);
            List<Vector2> validSpots = new List<Vector2>
            {
                new Vector2(-2.88f, -0.32f), 
                new Vector2(-1.6f, 0.32f),
                new Vector2(3.52f, 1.6f),
                new Vector2(0.32f, -1.6f),
                new Vector2(1.6f, -0.32f),
                new Vector2(0.32f, 0.32f),
                new Vector2(-0.96f, -0.96f),
                new Vector2(3.52f, -0.32f),
                new Vector2(-1.92f, 1.6f),
                new Vector2(0f, 1.6f),
                new Vector2(0.96f, 0.96f),
                new Vector2(1.92f, -1.28f),
                new Vector2(4.16f, -1.6f),
                new Vector2(-3.84f, -0.64f),
                new Vector2(4.48f, 1.6f)
            };
            Shuffle(itemsToSpawn);
            Shuffle(validSpots);
            for (int i = 0; i < itemsToSpawn.Count; i++)
            {
                if (i < validSpots.Count)
                {
                    Instantiate(itemsToSpawn[i], validSpots[i], Quaternion.identity);
                }
            }
        }
        private void AddItems(List<GameObject> list, GameObject prefab, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(prefab);
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