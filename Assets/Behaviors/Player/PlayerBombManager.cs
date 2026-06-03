using Base_Scripts;
using UnityEngine;

namespace Behaviors.Player
{
    public class PlayerBombManager : MonoBehaviour
    {
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] int playerExplosionRadius = 1;
        [SerializeField] int maxBombs = 1;
        
        private float _gridSize = 0.32f;
        private int _bombsPlaced;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (_bombsPlaced < maxBombs)
                {
                    DropBomb();
                }
            }
        }
        public void IncreaseBlast()
        {
            playerExplosionRadius++;
        }
        private void DropBomb()
        {
            // Snap to grid
            float snappedX = Mathf.Round(transform.position.x / _gridSize) * _gridSize;
            float snappedY = Mathf.Round(transform.position.y / _gridSize) * _gridSize;
            Vector3 snappedPos = new Vector3(snappedX, snappedY, transform.position.z);
            // Drop bomb
            GameObject newBomb = Instantiate(bombPrefab, snappedPos, Quaternion.identity);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.bombDrop);
            _bombsPlaced++;
            Bomb.Bomb bomb = newBomb.GetComponent<Bomb.Bomb>();
            bomb.ChangeExplosionRadius(playerExplosionRadius);
            bomb.SetOwner(this);
        }
        public void OnBombExploded()
        {
            _bombsPlaced--;
            if (_bombsPlaced < 0) _bombsPlaced = 0;
        }
        public void AddMaxBombs()
        {
            maxBombs++;
        }
        public void ResetMaxBombs()
        {
            maxBombs = 1;
        }
        public void AddExplosionRadius()
        {
            playerExplosionRadius++;
        }
        public void ResetExplosionRadius()
        {
            playerExplosionRadius = 1;
        }
    }
}

