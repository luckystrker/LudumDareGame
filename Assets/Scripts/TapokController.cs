using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;  // Точки патрулирования
    private int currentPatrolIndex;

    public float moveSpeed = 4.0f;

    void Update()
    {
        currentPatrolIndex = Move(this.transform, currentPatrolIndex);
    }

    // Our movement method; assuming called on Update.
    public int Move(Transform they, int currentWaypoint)
    {
        // Move transform.
        they.position = Vector2.MoveTowards(
            they.position,
            patrolPoints[currentWaypoint].position,  // Используем позицию трансформа
            moveSpeed * Time.deltaTime
        );
        
        // Check if AI is close enough to iterate next waypoint.
        if(Vector2.Distance(
            they.position,
            patrolPoints[currentWaypoint].position) < 0.1f)  // Используем позицию трансформа
        {
            currentWaypoint++;
            // Reset AI waypoint.
            if(currentWaypoint == patrolPoints.Length) currentWaypoint = 0;
            // Return our current waypoint.
        }
        return currentWaypoint;
    }
}