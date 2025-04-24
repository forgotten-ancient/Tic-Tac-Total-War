using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class boardManager : MonoBehaviour
{
    private List<CellManager> squareScripts = new List<CellManager>(); // Store the script components directly
    private bool[] playerOwnedGrid; // Initialize without a fixed size
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Square"))
            {
                // Get the script component from the child
                CellManager childScript = child.GetComponent<CellManager>();

                // Check if the script component exists
                if (childScript != null)
                {
                    squareScripts.Add(childScript);
                    Debug.Log($"Found Square: {child.name} with script {childScript.GetType().Name}");
                }
                else
                {
                    Debug.LogError($"Child with tag 'Square' ({child.name}) does not have the script 'CellManager' attached!");
                }
            }
        }

        playerOwnedGrid = new bool[squareScripts.Count]; // Initialize based on found cells

        // Optional: You can now iterate through the stored scripts
        foreach (CellManager script in squareScripts)
        {
            // Perform any initial setup or data retrieval from the child scripts here
            Debug.Log("Child script: "+script);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space");

            // Retrieve player ownership states
            for (int i = 0; i < squareScripts.Count; i++)
            {
                playerOwnedGrid[i] = squareScripts[i].isPlayerOwned;
                Debug.Log($"Is {squareScripts[i].gameObject.name} player owned: [{playerOwnedGrid[i]}]");
            }

            // Check for win condition
            bool isPlayerWin = winConditionMet(playerOwnedGrid);
            Debug.Log($"Win condition met? [{isPlayerWin}]");
        }
    }

    bool winConditionMet(bool[] array)
    {
        if (array[0] && array[1] && array[2]) return true;
        if (array[3] && array[4] && array[5]) return true;
        if (array[6] && array[7] && array[8]) return true;
        if (array[0] && array[3] && array[6]) return true;
        if (array[1] && array[4] && array[7]) return true;
        if (array[2] && array[5] && array[8]) return true;
        if (array[0] && array[4] && array[8]) return true;
        if (array[6] && array[4] && array[2]) return true;

        return false;
    }

    public bool checkWinCondition() {
        for (int i = 0; i < squareScripts.Count; i++)
        {
            playerOwnedGrid[i] = squareScripts[i].isPlayerOwned;
            // Debug.Log($"Is {squareScripts[i].gameObject.name} player owned: [{playerOwnedGrid[i]}]");
        }
        return winConditionMet(playerOwnedGrid);
    }
}