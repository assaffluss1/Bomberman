using Base_Scripts;
using UnityEngine;

namespace Behaviors.Enemies
{
    public class EnemyAnimations : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private bool _isDead;
        private float _deathDelay = 3f;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            if (_isDead) return;
            Vector2 direction = _rb.linearVelocity.normalized;
            _animator.SetFloat("InputX", direction.x);
            _animator.SetFloat("InputY", direction.y);
        }
        public void TriggerDeathAnimation()
        {
            if (_isDead) return;
            _isDead = true;
            GetComponent<EnemyMovement>().enabled = false;
            _animator.SetTrigger("OnHit");
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(DestroyEnemyObject), _deathDelay);
        }
        public void DestroyEnemyObject()
        {
            GameManager.Instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}
