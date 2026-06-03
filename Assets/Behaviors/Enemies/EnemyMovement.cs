using UnityEngine;
namespace Behaviors.Enemies
{
    public class EnemyMovement : AIMovementBase
    {
        protected override void FindTarget()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                TargetTransform = player.transform;
            }
        }
    }
}