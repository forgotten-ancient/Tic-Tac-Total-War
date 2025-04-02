using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PlayerUnitPrefab;
    [SerializeField] private GameObject EnemyUnitPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.y; // Set z to the camera's height
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.y = 0; // Ensure y is 0 for ground level

            Instantiate(PlayerUnitPrefab, worldPosition, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.y; // Set z to the camera's height
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.y = 0; // Ensure y is 0 for ground level

            Instantiate(EnemyUnitPrefab, worldPosition, Quaternion.identity);
        }
    }
}
