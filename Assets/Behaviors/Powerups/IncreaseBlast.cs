using Base_Scripts;
using Behaviors.Player;
using UnityEngine;

namespace Behaviors.Powerups
{
    public class IncreaseBlast : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerBombManager player = other.GetComponent<PlayerBombManager>();
            if (player != null && other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.powerupPickup);
                player.AddExplosionRadius();
                GameManager.Instance.AddScore(1000);
                Destroy(gameObject);
            }
        }
    }
}

