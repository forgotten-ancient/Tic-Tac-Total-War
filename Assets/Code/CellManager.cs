using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    //Insert colors for the player and enemy
    [SerializeField] Material PlayerMaterial;
    [SerializeField] Material EnemyMaterial;

    //Create 4 lists, one of available units for each side, and one for the units currently fighting
    private List<GameObject> PlayerUnits;
    private List<GameObject> EnemyUnits;
    private List<GameObject> FightingPlayerUnits;
    private List<GameObject> FightingEnemyUnits;
    // A private boolean to track if the cell is owned by the player or enemy. Add a getter so its read-only from other scripts.
    private bool PlayerOwned = false;
    public bool isPlayerOwned
    {
        get { return PlayerOwned; }
    }
    //Get the renderer
    private Renderer cellRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get the renderer component of the cell
        cellRenderer = gameObject.GetComponent<Renderer>();
        //Initialize the lists
        PlayerUnits = new List<GameObject>();
        EnemyUnits = new List<GameObject>();
        FightingPlayerUnits = new List<GameObject>();
        FightingEnemyUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //cyle through the lists and remove any null units
        for (int i = PlayerUnits.Count - 1; i >= 0; i--)
        {
            if (PlayerUnits[i] == null)
            {
                PlayerUnits.RemoveAt(i);
            }
        }
        for (int i = EnemyUnits.Count - 1; i >= 0; i--)
        {
            if (EnemyUnits[i] == null)
            {
                EnemyUnits.RemoveAt(i);
            }
        }
        for (int i = FightingPlayerUnits.Count - 1; i >= 0; i--)
        {
            if (FightingPlayerUnits[i] == null)
            {
                FightingPlayerUnits.RemoveAt(i);
            }
        }
        for (int i = FightingEnemyUnits.Count - 1; i >= 0; i--)
        {
            if (FightingEnemyUnits[i] == null)
            {
                FightingEnemyUnits.RemoveAt(i);
            }
        }
        //Switch cell ownership
        if (!PlayerOwned){
            cellRenderer.material = EnemyMaterial;
            if (EnemyUnits.Count <= 0 && FightingEnemyUnits.Count <= 0 && PlayerUnits.Count > 0){
                PlayerOwned = true;
            }
        }
        if (PlayerOwned){
            cellRenderer.material = PlayerMaterial;
            if (PlayerUnits.Count <= 0 && FightingPlayerUnits.Count <= 0 && EnemyUnits.Count > 0){
                PlayerOwned = false;
            }
        }

        ////If there are non-fighting units on both sides, start a fight
        if (PlayerUnits.Count > 0 && EnemyUnits.Count > 0){
            //Pick a non-fighting unit from each side
            GameObject playerFighter = PlayerUnits[0];
            GameObject enemyFighter = EnemyUnits[0];
            //Make sure they exist
            if (playerFighter == null || enemyFighter == null){
                return;
            }
            //Remove them from the available units list and add them to the fighting units list
            PlayerUnits.Remove(playerFighter);
            EnemyUnits.Remove(enemyFighter);
            FightingPlayerUnits.Add(playerFighter);
            FightingEnemyUnits.Add(enemyFighter);
            //Start the fight coroutine, passing the two units
            StartCoroutine(RunFight(playerFighter, enemyFighter));
        }
    }

//Combat function
    private IEnumerator RunFight(GameObject player, GameObject enemy){
        //Get the unit managers and navmesh agents for both units
        UnitManager playerManager = player.GetComponent<UnitManager>();
        UnitManager enemyManager = enemy.GetComponent<UnitManager>();
        NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
        NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
        //Find the meeting point between the two units
        Vector3 meetingPoint = (playerAgent.transform.position + enemyAgent.transform.position) / 2;
        //Set the destination for both units to the meeting point
        playerAgent.SetDestination(meetingPoint);
        enemyAgent.SetDestination(meetingPoint);
        //Wait for both units to reach the meeting point

        //Keep running damage until one of the units is dead
        while (playerManager.health >= 1 && enemyManager.health >= 1){
            //Determine who does damage first
            bool playerGoesFirst = Random.Range(0, 1) == 0;
            //Wait half a second
            yield return new WaitForSeconds(0.5f);
            //Deal damage to the units, and break out of the loop if one of them dies
            if (!playerGoesFirst){
                playerManager.health -= Random.Range(3, 5);
                if (playerManager.health <= 0){
                    break;
                }
                enemyManager.health -= Random.Range(3, 5);
            }else{
                enemyManager.health -= Random.Range(3, 5);
                if (enemyManager.health <= 0){
                    break;
                }
                playerManager.health -= Random.Range(3, 5);
            }
        }
        //One of the units should be dead, so remove both from the fighting lists
        FightingPlayerUnits.Remove(player);
        FightingEnemyUnits.Remove(enemy);
        //Destroy the dead unit and add the other to the appropriate list
        if (playerManager.health <= 0){
            EnemyUnits.Add(enemy);
            Destroy(player);
        }
        if (enemyManager.health <= 0){
            PlayerUnits.Add(player);
            Destroy(enemy);
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
        if (!unitManager.AssignedToCell)
        {
            if (unitManager.isPlayerUnit)
            {
                PlayerUnits.Add(unit);
            }
            else
            {
                EnemyUnits.Add(unit);
            }
            unitManager.AssignedToCell = true;
        }
    }
    //Remove the unit from the appropriate list
    private void ReleaseUnit(Collider unitCol)
    {
        GameObject unit = unitCol.gameObject;
        UnitManager unitManager = unit.GetComponent<UnitManager>();
        if (unitManager.AssignedToCell)
        {
            if (unitManager.isPlayerUnit)
            {
                PlayerUnits.Remove(unit);
            }
            else
            {
                EnemyUnits.Remove(unit);
            }
            unitManager.AssignedToCell = false;

        }
    }
}
