using UnityEngine;


public class Destination : MonoBehaviour
{
    [SerializeField] private float maxAttackTime = 3f; // Maximum time to wait before attacking
    [SerializeField] private float minAttackTime = 1f; // Minimum time to wait before attacking
    [SerializeField] private float attackTime = 1f; // starting attack time
    
    [SerializeField] private int maxEnemiesToAttack = 5; // Maximum number of enemies to attack at once
    [SerializeField] private int minEnemiesToAttack = 1; // Minimum number of enemies to attack at once
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attackTime <= 0)
        {
            EnemyAI[] allEnemies = Object.FindObjectsByType<EnemyAI>(FindObjectsSortMode.None); // Finds all EnemyAI instances

            int enemiesToAttack = Random.Range(minEnemiesToAttack, maxEnemiesToAttack + 1); // Randomly select number of enemies to attack
            for (int i = 0; i < enemiesToAttack; i++)
            {
                int randomIndex = Random.Range(0, allEnemies.Length); // Select a random enemy from the array
                EnemyAI selectedEnemy = allEnemies[randomIndex]; // Get the selected enemy
                selectedEnemy.MoveEnemies(); // Call MoveEnemies() on the selected enemy
            }

            attackTime = Random.Range(minAttackTime, maxAttackTime); // Reset attack time to a random value between min and max
        }
        else
        {
            attackTime -= Time.deltaTime; // Decrease attack time by the time since last frame
        }
    }
}
