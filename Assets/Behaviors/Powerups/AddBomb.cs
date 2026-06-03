using Base_Scripts;
using UnityEngine;
using Behaviors.Player;

namespace Behaviors.Powerups
{
    public class AddBomb : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerBombManager player = other.GetComponent<PlayerBombManager>();
            if (player != null && other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.powerupPickup);
                player.AddMaxBombs();
                GameManager.Instance.AddScore(1000);
                Destroy(gameObject);
            }
        }
    }
}


