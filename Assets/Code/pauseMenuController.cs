using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenuController : MonoBehaviour
{
    // serialized variable to hold the pause menu UI Game Object
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button exitButton;
    // Private boolean to keep track of whether the game is paused or not
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure that pause menu is initially hidden
        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(false);
        } else {
            Debug.LogError("Pause Menu UI GameObject is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                Debug.Log("Unpausing...");
                UnpauseGame();
            } else {
                Debug.Log("Pausing...");
                PauseGame();
            }
        }
        // add event listener for onclick to reset button
        resetButton.onClick.AddListener(ResetGame);
        // add event listener for onclick to exit button
        exitButton.onClick.AddListener(ExitApplication);

    }
    
    // function to pause game
    void PauseGame() {
        Debug.Log("PauseGame called");
        // set time scale to 0 to stop time
        Time.timeScale = 0f;
        // make the pause menu UI visible.
        pauseMenuUI.SetActive(true);
        // update the paused state;
        isPaused = true;
    }

    // function to unpause game
    void UnpauseGame() {
        Debug.Log("Unpause Game Called");
        // Set time scale to 1 to resume time
        Time.timeScale = 1f;
        // hide pause menu UI
        pauseMenuUI.SetActive(false);
        // Update the paused state
        isPaused = false;
    }

    public void ResetGame() {
        Debug.Log("Reset Called");
        // Unpause the game before reloading the scene
        Time.timeScale = 1f;
        // hide pause menu UI
        pauseMenuUI.SetActive(false);
        // set paused state to false
        isPaused = false;
        // reload current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This function will be called when the button is clicked.
    public void ExitApplication()
    {
        #if UNITY_EDITOR
            // If running in the Unity Editor, stop playing the scene.
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If running in a built application, quit the application.
            Application.Quit();
        #endif

        Debug.Log("Exiting Application"); // Optional: for debugging
    }
}
