using Base_Scripts;
using UnityEngine;

namespace Behaviors.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f; 
        
        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        private float _stepTimer;
        private float _stepRate = 0.3f;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            // Don't allow diagonal movement
            if (horizontalInput != 0)
            {
                verticalInput = 0;
            }
            _moveInput = new Vector2(horizontalInput, verticalInput).normalized;
            HandleFootsteps();
        }
        void FixedUpdate()
        {
            _rb.linearVelocity = _moveInput * moveSpeed;
        }
        public void AddMoveSpeed()
        {
            moveSpeed += 0.2f;
        }
        public void ResetMoveSpeed()
        {
            moveSpeed = 5f;
        }
        private void HandleFootsteps()
        {
            if (_moveInput == Vector2.zero) 
            {
                return;
            }
            _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0)
            {
                if (Mathf.Abs(_moveInput.x) > 0)
                {
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlaySFX(SoundManager.Instance.stepLeftRightSound);
                }
                else if (Mathf.Abs(_moveInput.y) > 0)
                {
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlaySFX(SoundManager.Instance.stepUpDownSound);
                }
                _stepTimer = _stepRate; 
            }
        }
    }
}
