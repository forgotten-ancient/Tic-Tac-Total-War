using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour
{
    [SerializeField] GameObject endGameUI = null;
    [SerializeField] GameObject boardManagerHolder = null;
    [SerializeField] GameObject gameManagerHolder = null;
    private bool isPaused = false;
    private bool winCheck = false;
    private Text title;
    private Button resetButton;
    private GameManager game;
    private boardManager board;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!endGameUI) {
            Debug.LogError("WinLoseManager requires a UI to operate.");
        } else {
            title = GameObject.Find("Title").GetComponent<Text>(); 
            resetButton = GameObject.Find("NewGame").GetComponent<Button>();
            resetButton.onClick.AddListener(Reset);
        }

        if(!boardManagerHolder || !gameManagerHolder) {
            Debug.LogError("WinLoseManager requires a board manager and shop manager game object reference");
        } else {
            game = gameManagerHolder.GetComponent<GameManager>();
            board = boardManagerHolder.GetComponent<boardManager>();
            Debug.Log(game);
            Debug.Log(board);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (endGameUI && board && game) {
            // if win condition check is true, end game and execute win condition code
            // else if (no soldiers left on board) and (money less than cheapest soldier {25?}),
            // end game and execute lose condition code
            winCheck = board.checkWinCondition();
            if (winCheck) {
                // win condition execute
                winCondition();
            } else if (game.coins < 25 && game.currentSoldiers <=0) {
                // lose condition execute
                loseCondition();
            }

        } else {
            Debug.Log(board);
            Debug.Log(game);
            Debug.LogError("UI, board, or game are missing in winLoseCondition.cs: Line 58");
        }
    }

    // function to pause game
    void PauseGame() {
        Debug.Log("PauseGame called");
        // set time scale to 0 to stop time
        Time.timeScale = 0f;
        // make the pause menu UI visible.
        endGameUI.SetActive(true);
        // update the paused state;
        isPaused = true;
    }

    // function to unpause game
    void Reset() {
        Debug.Log("Reset Called");
        // Set time scale to 1 to resume time
        Time.timeScale = 1f;
        // hide pause menu UI
        endGameUI.SetActive(false);
        // Update the paused state
        isPaused = false;
        // reload current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void winCondition() {
        PauseGame();
        title.text = "You Win!";
        
    }

    void loseCondition() {
        PauseGame();
        title.text = "You Lose.";

    }
}
