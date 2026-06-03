using Base_Scripts;
using UnityEngine;

namespace Behaviors.Door
{
    public class UseDoor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length == 0)
                {
                    GameManager.Instance.WinGame();
                }
            }
        }
    }
}

