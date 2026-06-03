using Base_Scripts;
using UnityEngine;

namespace Behaviors.Bomb.ChickenBomb
{
    public class ChickenBomb : BombBase
    {
        private bool _isDead;
        
        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            // Ignore player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Collider2D myCollider = GetComponent<Collider2D>();
                Collider2D playerCollider = player.GetComponent<Collider2D>();
                if (myCollider != null && playerCollider != null)
                {
                    Physics2D.IgnoreCollision(myCollider, playerCollider);
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_isDead) return;
            if (other.gameObject.CompareTag("Enemy"))
            {
                ExplodeChicken();
            }
        }
        private void ExplodeChicken()
        {
            _isDead = true;
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Static;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.chickenHit);
            TriggerExplosion();
        }
    }
}
