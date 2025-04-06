using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int MaximumEnemiesToStart = 8; // Maximum number of enemies to spawn
    [SerializeField] private int MinimunEnemiesToStart = 3; // Minimum number of enemies to spawn
    [SerializeField] private GameObject Enemy; // Prefab of the enemy to spawn
    [SerializeField] private CellManager[] Cells;
    [SerializeField] private float SpawnTime = 3f; // Time interval between enemy spawns
    [SerializeField] private float SpawnTimeMax = 6f; // Maximum spawn time
    [SerializeField] private float SpawnTimeMin = 3f; // Minimum spawn time
    [SerializeField] private float SpawnChance = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (CellManager cell in Cells) // Iterate through each cell object
        {
            int randomAmount = Random.Range(MinimunEnemiesToStart, MaximumEnemiesToStart); // Randomly select the number of enemies to spawn
            for (int i = 0; i < randomAmount; i++)
            {
                GameObject unit = Instantiate(Enemy, cell.transform.position, Quaternion.identity); // Spawn the enemy at the cell's position
                Vector3 move = new Vector3(
                    cell.transform.position.x+0.1f, 
                    cell.transform.position.y, 
                    cell.transform.position.z); // Create a vector to move the enemy a little bit
                NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the enemy

                unitAgent.SetDestination(move); // Set the destination of the enemy to the cell's transform
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTime > 0)
        {
            SpawnTime -= Time.deltaTime; // Decrease the spawn time by the time since the last frame
        }
        else
        {
            foreach (CellManager cell in Cells) // Iterate through each cell object
            {
                float randomValue = Random.Range(0f, 1f); // Generate a random value between 0 and 1
                if (randomValue < SpawnChance && !cell.isPlayerOwned) // Check if the random value is less than the spawn chance
                {
                    GameObject unit = Instantiate(Enemy, cell.transform.position, Quaternion.identity); // Spawn the enemy at the cell's position
                    Vector3 move = new Vector3(
                        cell.transform.position.x + 0.1f,
                        cell.transform.position.y,
                        cell.transform.position.z); // Create a vector to move the enemy a little bit
                    NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the enemy

                    unitAgent.SetDestination(move); // Set the destination of the enemy to the cell's transform
                }
            }
            SpawnTime = Random.Range(SpawnTimeMin, SpawnTimeMax); // Reset the spawn time to a random value between the minimum and maximum
        }
    }
}
