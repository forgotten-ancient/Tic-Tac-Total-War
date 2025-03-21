using UnityEngine;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    private List<GameObject> PlayerUnits;
    private List<GameObject> EnemyUnits;
    private List<GameObject> FightingUnits;

    private bool PlayerOwned = false;

    public bool isPlayerOwned
    {
        get { return PlayerOwned; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialize the lists
        PlayerUnits = new List<GameObject>();
        EnemyUnits = new List<GameObject>();
        FightingUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug Stuff
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(gameObject.name + " Player Units: " + PlayerUnits.Count + " Enemy Units: " + EnemyUnits.Count + " PlayerOwned: " + PlayerOwned);
        }

        //Switch cell ownership
        if (!PlayerOwned){
            if (EnemyUnits.Count <= 0 && PlayerUnits.Count > 0){
                PlayerOwned = true;
            }
        }
        if (PlayerOwned){
            if (PlayerUnits.Count <= 0 && EnemyUnits.Count > 0){
                PlayerOwned = false;
            }
        }
    }


//Logic for adding and removing units from the cell memory
    //When a unit enters the cell, run ClaimUnit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            ClaimUnit(other);
        }
    }
    //When a unit exits the cell, run ReleaseUnit
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            ReleaseUnit(other);
        }
    }
    //Add the unit to the appropriate list
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
    //Remove the unit from the appropriate list
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
