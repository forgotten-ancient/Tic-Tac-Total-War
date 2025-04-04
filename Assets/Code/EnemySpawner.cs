using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int MaximumEnemiesToStart = 8; // Maximum number of enemies to spawn
    [SerializeField] private int MinimunEnemiesToStart = 3; // Minimum number of enemies to spawn
    [SerializeField] private GameObject Enemy; // Prefab of the enemy to spawn
    private GameObject[] SquareObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SquareObjects = GameObject.FindGameObjectsWithTag("Square"); // Get all objects with the given tag
        foreach (GameObject square in SquareObjects) // Iterate through each square object
        {
            int randomAmount = Random.Range(MinimunEnemiesToStart, MaximumEnemiesToStart); // Randomly select the number of enemies to spawn
            for (int i = 0; i < randomAmount; i++)
            {
                GameObject unit = Instantiate(Enemy, square.transform.position, Quaternion.identity); // Spawn the enemy at the square's position
                Vector3 move = new Vector3(
                    square.transform.position.x+0.1f, 
                    square.transform.position.y, 
                    square.transform.position.z); // Create a vector to move the enemy a little bit
                NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the enemy

                unitAgent.SetDestination(move); // Set the destination of the enemy to the square's transform
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
