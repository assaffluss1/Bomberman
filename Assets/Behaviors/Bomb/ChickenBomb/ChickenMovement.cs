using UnityEngine;

namespace Behaviors.Bomb.ChickenBomb
{
    public class ChickenMovement : AIMovementBase
    {
        protected override void Awake()
        {
            base.Awake();
            chaseChance = 1f;
        }
        protected override void FindTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }
            if (closestEnemy != null)
            {
                TargetTransform = closestEnemy;
            }
        }
    }
}
