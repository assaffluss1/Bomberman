using Base_Scripts;
using Behaviors.Player;
using UnityEngine;

namespace Behaviors.Powerups
{
    public class IncreaseSpeed : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null && other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.powerupPickup);
                player.AddMoveSpeed();
                GameManager.Instance.AddScore(1000);
                Destroy(gameObject);
            }
        }
    }
}
