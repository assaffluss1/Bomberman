using Base_Scripts;
using Behaviors.Player;
using UnityEngine;

namespace Behaviors.Powerups
{
    public class Invinsibility : MonoBehaviour
    {
        private float _invincibleTime = 12f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHit player = other.GetComponent<PlayerHit>();
            if (player != null && other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.powerupPickup);
                player.ActivateInvincibility(_invincibleTime);
                GameManager.Instance.AddScore(1000);
                Destroy(gameObject);
            }
        }
    }
}