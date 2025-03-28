using UnityEngine;


public class Destination : MonoBehaviour
{
    [SerializeField] public bool playerControlled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            EnemyAI[] allEnemies = Object.FindObjectsByType<EnemyAI>(FindObjectsSortMode.None); // Finds all EnemyAI instances

            foreach (EnemyAI enemy in allEnemies)
            {
                enemy.MoveEnemies(); // Call MoveEnemies() on each enemy
            }
        }

    }
}
