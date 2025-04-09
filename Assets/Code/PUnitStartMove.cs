using UnityEngine;
using UnityEngine.AI;

public class PUnitStartMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] SquareObjects = GameObject.FindGameObjectsWithTag("Square"); // Get all objects with the given tag
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject Square in SquareObjects)
        {
            CellManager destination = Square.GetComponent<CellManager>();
            if (destination != null)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
