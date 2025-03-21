using UnityEngine;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    private List<GameObject> PlayerUnits;
    private List<GameObject> EnemyUnits;
    private bool isPlayerCell;
    private List<GameObject> FightingUnits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            ClaimUnit();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            ReleaseUnit();
        }
    }

    private void ClaimUnit(unit)
    {
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

    private void ReleaseUnit(unit)
    {
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
