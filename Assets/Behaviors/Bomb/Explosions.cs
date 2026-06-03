using Behaviors.Enemies;
using Behaviors.Player;
using UnityEngine;

namespace Behaviors.Bomb
{
    public class Explosion : MonoBehaviour
    {
        public float lifetime = 1.1f; 
        
        void Start()
        {
            Destroy(gameObject, lifetime);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyAnim = other.GetComponent<EnemyAnimations>();
            if (enemyAnim != null)
            {
                enemyAnim.TriggerDeathAnimation();
            }
            var playerHit = other.GetComponent<PlayerHit>(); 
            if (playerHit != null)
            {
                playerHit.HandleDeath();
            }
        }
    }
}

