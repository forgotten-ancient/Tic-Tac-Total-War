using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public void MoveEnemies()
    {
        GameObject[] SquareObjects = GameObject.FindGameObjectsWithTag("Square"); // Get all objects with the given tag
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject Square in SquareObjects)
        {
            Destination destination = Square.GetComponent<Destination>();
            if (destination != null && destination.playerControlled)
            {
                Transform potentialTarget = Square.transform;
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        }

        if (bestTarget != null)
        {
            agent.SetDestination(bestTarget.position);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= 0.01f){
            agent.ResetPath();
        }
    }
}
