using UnityEngine;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    [SerializeField] private GameObject CellA;
    [SerializeField] private GameObject CellB;

    private List<GameObject> unitsInCellA;
    private List<GameObject> unitsInCellB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            float distanceA = Vector3.Distance(collision.transform.position, CellA.transform.position);
            float distanceB = Vector3.Distance(collision.transform.position, CellB.transform.position);

            if (distanceA < distanceB)
            {
                unitsInCellA.Add(collision.gameObject);
                Debug.Log("Unit added to Cell A");
            }
            else
            {
                unitsInCellB.Add(collision.gameObject);
                Debug.Log("Unit added to Cell B");
            }
        }
    }
}
