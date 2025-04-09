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
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100)){
                Instantiate(PlayerUnitPrefab, hit.point, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100)){
                Instantiate(EnemyUnitPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
