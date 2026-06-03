using System.Collections;
using Base_Scripts;
using UnityEngine;

namespace Behaviors.Player
{
    public class PlayerHit : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer; 
        [SerializeField] private float deathDelay = 1.4f;
        
        private PlayerAnimations _playerAnimations;
        private SpriteRenderer _spriteRenderer;
        
        private bool _isDead;
        private bool _isInvincible;
        
        void Awake()
        {
            _playerAnimations = GetComponent<PlayerAnimations>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if (_isDead) return;
            if (_isInvincible) return;
            if (Physics2D.OverlapCircle(transform.position, 0.01f, enemyLayer))
            {
                HandleDeath();
            }
        }
        public void HandleDeath()
        {
            if(_isInvincible) return;
            if (_isDead) return;
            _isDead = true;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.playerHit);
            _playerAnimations.TriggerDeathAnimation();
            Invoke(nameof(CallLoseLife), deathDelay);
        }
        private void CallLoseLife()
        {
            GameManager.Instance.LoseLife();
        }
        public void ActivateInvincibility(float duration)
        {
            StartCoroutine(InvincibilityRoutine(duration));
        }
        private IEnumerator InvincibilityRoutine(float duration)
        {
            _isInvincible = true;
            _spriteRenderer.color = Color.yellow;
            float blinkDuration = 3f;
            float solidDuration = duration - blinkDuration;
            // Make yellow
            if (solidDuration > 0)
            {
                yield return new WaitForSeconds(solidDuration);
            }
            float blinkTimer = 0f;
            float toggleSpeed = 0.1f;
            // Make blink until timer is up
            while (blinkTimer < blinkDuration)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;
                yield return new WaitForSeconds(toggleSpeed);
                blinkTimer += toggleSpeed;
            }
            _spriteRenderer.enabled = true;
            _isInvincible = false;
            _spriteRenderer.color = Color.white;
        }
    }
}

