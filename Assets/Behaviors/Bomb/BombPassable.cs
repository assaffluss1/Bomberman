using UnityEngine;

namespace Behaviors.Bomb
{
    public class BombPassable : MonoBehaviour
    {
        private Collider2D _myCollider;

        void Awake()
        {
            _myCollider = GetComponent<Collider2D>();
            _myCollider.isTrigger = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _myCollider.isTrigger = false;
            }
        }
    }
}
