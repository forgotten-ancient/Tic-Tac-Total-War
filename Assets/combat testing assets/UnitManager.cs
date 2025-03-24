using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private bool PlayerUnit;
    [SerializeField] private Slider healthBar;
    [SerializeField] public int health =  20;
    public bool AssignedToCell = false;
    private NavMeshAgent agent;

    //PlayerUnit getter
    public bool isPlayerUnit
    {
        get { return PlayerUnit; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get the NavMeshAgent component
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //animate health bar
        healthBar.value = health;
        
        //Check if the unit has reached its destination
        if (agent.hasPath && agent.remainingDistance < 1.0f)
        {
            agent.ResetPath();
        }
        //Debug stuff, basically allows you to move the units around before we have AI to do it for us
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
