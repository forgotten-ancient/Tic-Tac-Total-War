using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    [SerializeField] Material PlayerMaterial;
    [SerializeField] Material EnemyMaterial;
    private List<GameObject> PlayerUnits;
    private List<GameObject> EnemyUnits;
    private List<GameObject> FightingPlayerUnits;
    private List<GameObject> FightingEnemyUnits;

    private bool PlayerOwned = false;

    public bool isPlayerOwned
    {
        get { return PlayerOwned; }
    }

    private Renderer cellRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        //Manage combat
        if (PlayerUnits.Count > 0 && EnemyUnits.Count > 0){
            GameObject playerFighter = PlayerUnits[0];
            GameObject enemyFighter = EnemyUnits[0];
            PlayerUnits.Remove(playerFighter);
            EnemyUnits.Remove(enemyFighter);
            FightingPlayerUnits.Add(playerFighter);
            FightingEnemyUnits.Add(enemyFighter);
            StartCoroutine(RunFight(playerFighter, enemyFighter));
        }
    }

//Combat function
    private IEnumerator RunFight(GameObject player, GameObject enemy){
        UnitManager playerManager = player.GetComponent<UnitManager>();
        UnitManager enemyManager = enemy.GetComponent<UnitManager>();
        NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
        NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();

        Vector3 meetingPoint = (playerAgent.transform.position + enemyAgent.transform.position) / 2;
        playerAgent.SetDestination(meetingPoint);
        enemyAgent.SetDestination(meetingPoint);
        
        while (playerManager.health > 0 && enemyManager.health > 0){
            bool playerGoesFirst = Random.Range(0, 1) == 0;
            yield return new WaitForSeconds(0.5f);
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
            Debug.Log(player.name + " health: " + playerManager.health + " " + enemy.name + " health: " + enemyManager.health);
        }

        FightingPlayerUnits.Remove(player);
        FightingEnemyUnits.Remove(enemy);
        if (playerManager.health <= 0){
            Destroy(player);
            EnemyUnits.Add(enemy);
        }
        if (enemyManager.health <= 0){
            Destroy(enemy);
            PlayerUnits.Add(player);
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
