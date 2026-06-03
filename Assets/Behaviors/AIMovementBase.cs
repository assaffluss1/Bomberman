using System.Collections.Generic;
using UnityEngine;

namespace Behaviors
{
    public abstract class AIMovementBase : MonoBehaviour
    {
        [SerializeField] protected float speed = 0.7f;
        [SerializeField] protected float gridSize = 0.64f;
        [SerializeField] protected Vector2 gridOffset = new Vector2(0f, 0.32f);
        
        [Range(0f, 1f)] [SerializeField] protected float chaseChance = 0.5f;
        [SerializeField] protected float detectionRange = 5.0f;
        [Range(0f, 1f)] [SerializeField] protected float keepStraightChance = 0.85f;
        
        [SerializeField] protected float rayDistance = 0.25f;
        [SerializeField] protected float rayStartOffset = 0.2f;
        [SerializeField] protected LayerMask obstacleLayer;
        
        protected Transform TargetTransform;
        
        private Vector2 _currentDirection;
        private Rigidbody2D _rb;
        private Vector2 _lastDecisionTile;
        
        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        protected virtual void Start()
        {
            FindTarget(); 
            _lastDecisionTile = GetGridPosition(transform.position);
            PickDirection(false);
        }
        protected virtual void FixedUpdate()
        {
            _rb.linearVelocity = _currentDirection * speed;
            SnapToGridLane();
        }
        protected virtual void Update()
        {
            if (TargetTransform == null)
            {
                FindTarget();
            }
            // If stuck, pick new direction
            if (_currentDirection != Vector2.zero && _rb.linearVelocity.magnitude < 0.01f)
            {
                PickDirection(true);
            }
            // If there is an intersection, possibly change direction
            Vector2 currentGridPos = GetGridPosition(transform.position);
            Vector2 worldCenterOfTile = GetWorldPositionFromGrid(currentGridPos);
            if (Vector2.Distance(transform.position, worldCenterOfTile) < 0.05f)
            {
                if (currentGridPos != _lastDecisionTile)
                {
                    _lastDecisionTile = currentGridPos;
                    CheckIntersection();
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // If we hit something that is not target, turn around.
            if (TargetTransform == null || collision.gameObject != TargetTransform.gameObject)
            {
                PickDirection(true);
            }
        }
        protected void CheckIntersection()
        {
            List<Vector2> validMoves = GetValidDirections();
            if (validMoves.Count <= 2 && validMoves.Contains(_currentDirection))
            {
                return;
            }
            PickDirection(false);
        }
        protected void PickDirection(bool hitWall)
        {
            List<Vector2> validMoves = GetValidDirections();
            // Turn around if dead end
            if (validMoves.Count == 0)
            {
                _currentDirection = Vector2.zero;
                return;
            }
            // If we scrape a wall before grid snap, keep going
            if (hitWall && validMoves.Contains(_currentDirection))
            {
                return;
            }
            // Possible turn towards player
            if (TargetTransform != null && Random.value < chaseChance)
            {
                foreach (Vector2 dir in validMoves)
                {
                    if (IsTargetAligned(dir))
                    {
                        _currentDirection = dir;
                        return;
                    }
                }
            }
            // Usually keep going forward
            if (!hitWall && validMoves.Contains(_currentDirection))
            {
                if (Random.value < keepStraightChance)
                {
                    return;
                }
            }
            _currentDirection = validMoves[Random.Range(0, validMoves.Count)];
        }
        protected List<Vector2> GetValidDirections()
        {
            Vector2[] allDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            List<Vector2> valid = new List<Vector2>();
            foreach (Vector2 dir in allDirections)
            {
                Vector2 rayStart = (Vector2)transform.position + (dir * rayStartOffset);
                RaycastHit2D hit = Physics2D.Raycast(rayStart, dir, rayDistance, obstacleLayer);
                if (hit.collider == null)
                {
                    valid.Add(dir);
                }
            }
            return valid;
        }
        protected bool IsTargetAligned(Vector2 direction)
        {
            if (TargetTransform == null)
            {
                return false;
            }
            Vector2 rayStart = (Vector2)transform.position + (direction * rayStartOffset);
            RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, detectionRange);
            if (hit.collider != null)
            {
                return hit.collider.transform == TargetTransform;
            }
            return false;
        }
        private void SnapToGridLane()
        {
            if (_currentDirection == Vector2.zero) return;
            Vector2 currentPos = transform.position;
            Vector2 targetPos = currentPos;
            if (Mathf.Abs(_currentDirection.x) > 0)
            {
                float snappedY = Mathf.Round((currentPos.y - gridOffset.y) / gridSize) * gridSize + gridOffset.y;
                targetPos.y = Mathf.MoveTowards(currentPos.y, snappedY, speed * Time.fixedDeltaTime);
            }
            else if (Mathf.Abs(_currentDirection.y) > 0)
            {
                float snappedX = Mathf.Round((currentPos.x - gridOffset.x) / gridSize) * gridSize + gridOffset.x;
                targetPos.x = Mathf.MoveTowards(currentPos.x, snappedX, speed * Time.fixedDeltaTime);
            }
            _rb.position = targetPos;
        }
        private Vector2 GetGridPosition(Vector2 pos)
        {
            float x = Mathf.Round((pos.x - gridOffset.x) / gridSize);
            float y = Mathf.Round((pos.y - gridOffset.y) / gridSize);
            return new Vector2(x, y);
        }
        private Vector2 GetWorldPositionFromGrid(Vector2 gridPos)
        {
            float x = gridPos.x * gridSize + gridOffset.x;
            float y = gridPos.y * gridSize + gridOffset.y;
            return new Vector2(x, y);
        }
        // Abstract method to see which target each child object is chasing
        protected abstract void FindTarget();
    }
}
