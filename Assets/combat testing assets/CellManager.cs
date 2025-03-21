using UnityEngine;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    private List<GameObject> PlayerUnits;
    private List<GameObject> EnemyUnits;
    private List<GameObject> FightingUnits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerUnits = new List<GameObject>();
        EnemyUnits = new List<GameObject>();
        FightingUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(gameObject.name + " Player Units: " + PlayerUnits.Count + " Enemy Units: " + EnemyUnits.Count);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            ClaimUnit(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            ReleaseUnit(other);
        }
    }

    private void ClaimUnit(Collider unitCol)
    {
        GameObject unit = unitCol.gameObject;
        UnitManager unitManager = unit.GetComponent<UnitManager>();
        if (unitManager.isPlayerUnit)
        {
            PlayerUnits.Add(unit);
        }
        else
        {
            EnemyUnits.Add(unit);
        }
    }

    private void ReleaseUnit(Collider unitCol)
    {
        GameObject unit = unitCol.gameObject;
        UnitManager unitManager = unit.GetComponent<UnitManager>();
        if (unitManager.isPlayerUnit)
        {
            PlayerUnits.Remove(unit);
        }
        else
        {
            EnemyUnits.Remove(unit);
        }
    }
}
