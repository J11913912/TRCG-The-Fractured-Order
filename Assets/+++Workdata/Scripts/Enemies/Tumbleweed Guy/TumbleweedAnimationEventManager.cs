using UnityEngine;

public class TumbleweedAnimationEventManager : MonoBehaviour
{
    public EnemyNavMeshPatrol enemyNavMeshPatrol;
    
    public void TriggerStunned()
    {
        enemyNavMeshPatrol.HitWall();
    }
}
