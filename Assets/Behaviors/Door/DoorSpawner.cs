using System.Collections.Generic;
using UnityEngine;

namespace Behaviors.Door
{
    public class DoorSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject doorPrefab;
        
        void Start()
        {
            SpawnDoor();
        }
        private void SpawnDoor()
        {
            List<Vector2> validSpawnPoints = new List<Vector2>
            {
                new Vector2(1.92f, -0.64f),
                new Vector2(4.48f, 0.96f),
                new Vector2(0.32f, -1.6f),
            };
            Shuffle(validSpawnPoints);
            Instantiate(doorPrefab, validSpawnPoints[0], Quaternion.identity);
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
