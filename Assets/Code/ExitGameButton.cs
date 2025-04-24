using Unity.VisualScripting;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    void Update()
    {
        
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