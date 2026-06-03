using UnityEngine;

namespace Behaviors.Bricks
{
    public class BrickHit : MonoBehaviour
    {
        private float _destroyDelay = 0.5f;
        
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void DestroyBrick()
        {
            _animator.SetTrigger("Hit");
            Destroy(gameObject, _destroyDelay);
        }
    }
}

