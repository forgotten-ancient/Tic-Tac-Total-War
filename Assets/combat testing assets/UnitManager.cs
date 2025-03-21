using UnityEngine;
using UnityEngine.AI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] public bool isPlayerUnit;
    [SerializeField] private int health;
    [SerializeField] private int attack;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerUnit)
        {
            if (Input.GetMouseButtonDown(0)) // Left-click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit point is on the NavMesh
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
                    {
                        agent.SetDestination(navHit.position);
                    }
                }
            }
        }else{
            if (Input.GetMouseButtonDown(1)) // right-click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit point is on the NavMesh
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
                    {
                        agent.SetDestination(navHit.position);
                    }
                }
            }
        }
    }
}
