using Base_Scripts;
using Behaviors.Bricks;
using UnityEngine;

namespace Behaviors.Bomb
{
    public abstract class BombBase : MonoBehaviour
    {
        [SerializeField] protected int explosionRadius = 1;

        [SerializeField] protected GameObject blastMid;
        [SerializeField] protected GameObject blastTop;     
        [SerializeField] protected GameObject blastTopMid; 
        [SerializeField] protected GameObject blastBottom;  
        [SerializeField] protected GameObject blastBottomMid; 
        [SerializeField] protected GameObject blastRight;   
        [SerializeField] protected GameObject blastRightMid; 
        [SerializeField] protected GameObject blastLeft;    
        [SerializeField] protected GameObject blastLeftMid;

        [SerializeField] protected LayerMask wallLayer;
        
        private readonly float _gridSize = 0.32f;

        protected virtual void TriggerExplosion()
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.bombExplode);
            }
            Vector2 snappedPos = SnapToGrid(transform.position);
            Instantiate(blastMid, snappedPos, Quaternion.identity);
            SpawnArm(snappedPos, Vector2.up,    blastTopMid,    blastTop);
            SpawnArm(snappedPos, Vector2.down,  blastBottomMid, blastBottom);
            SpawnArm(snappedPos, Vector2.left,  blastLeftMid,   blastLeft);
            SpawnArm(snappedPos, Vector2.right, blastRightMid,  blastRight);
            Destroy(gameObject);
        }
        private Vector2 SnapToGrid(Vector2 rawPosition)
        {
            float snapX = Mathf.Round(rawPosition.x / _gridSize) * _gridSize;
            float snapY = Mathf.Round(rawPosition.y / _gridSize) * _gridSize;
            return new Vector2(snapX, snapY);
        }
        private void SpawnArm(Vector2 startPos, Vector2 direction, GameObject midPrefab, GameObject endPrefab)
        {
            for (int i = 1; i <= explosionRadius; i++)
            {
                Vector2 spawnPos = startPos + (direction * (i * _gridSize));
                Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.1f, wallLayer);
                if (hit != null)
                {
                    BrickHit brick = hit.GetComponent<BrickHit>();
                    if (brick != null)
                    {
                        brick.DestroyBrick();
                    }
                    break; 
                }
                if (i == explosionRadius)
                {
                    Instantiate(endPrefab, spawnPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(midPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
