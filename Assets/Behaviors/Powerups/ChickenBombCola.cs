using Base_Scripts;
using Behaviors.Player;
using UnityEngine;

namespace Behaviors.Powerups
{
    public class ChickenBombSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject chickenBombPrefab;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerBombManager player = other.GetComponent<PlayerBombManager>();
            if (player != null && other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.chickenSpawn);
                GameManager.Instance.AddScore(5000);
                Instantiate(chickenBombPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
