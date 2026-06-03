using UnityEngine;

namespace Behaviors.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private bool _isDead;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _animator.SetFloat("InputX", -1f);
            _animator.SetFloat("InputY", 0f);
        }
        void Update()
        {
            if (_isDead) return;
            bool isMoving = _rb.linearVelocity.magnitude > 0.1f;
            if (isMoving)
            {
                //Continue animations
                _animator.speed = 1;
                //Check direction
                _animator.SetFloat("InputX", _rb.linearVelocity.x);
                _animator.SetFloat("InputY", _rb.linearVelocity.y);
            }
            else
            {
                //Stop animation on last frame
                _animator.speed = 0;
            }
        }
        public void TriggerDeathAnimation()
        {
            if (_isDead) return;
            _isDead = true;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerBombManager>().enabled = false;
            _rb.linearVelocity = Vector2.zero;
            _animator.speed = 1;
            _animator.SetTrigger("OnHit");
        }
    }
}
