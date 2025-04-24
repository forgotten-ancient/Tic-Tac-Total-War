using UnityEngine;

public class playerControlScript : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject projectilePrefab; // Assign your projectile prefab here
    [SerializeField] private Transform projectileSpawnPoint; // An empty GameObject where the projectile will spawn
    [SerializeField] private float minLaunchVelocity = 1.0f;
    [SerializeField] private float maxLaunchVelocity = 5.0f;
    [SerializeField] private float stepAmplifier = 1.0f;
    [SerializeField] private float velocityAmplifier = 100.0f;
    [SerializeField] private ChargeBarUIController chargeBarUIController;
    private float currentLaunchVelocity;
    private bool isCharging = false;
    private float chargeStartTime;

    [SerializeField] private GameObject pauseMenuUI; // Assign your pause menu UI GameObject here
    private bool isPaused = false;

    void Update()
    {
        // Player rotation based on mouse position
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 worldMousePosition = hit.point;
            Vector3 direction = worldMousePosition - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }

        // Handle projectile launch
        if (Input.GetMouseButtonDown(0) && !isPaused) // Left mouse button down
        {
            isCharging = true;
            chargeStartTime = Time.time;
            currentLaunchVelocity = minLaunchVelocity;

            if (chargeBarUIController != null)
            {
                chargeBarUIController.StartCharging();
            }
            else
            {
                Debug.LogWarning("ChargeBarUIController not assigned in the Inspector of the Player!");
            }
        }

        if (isCharging && Input.GetMouseButton(0) && !isPaused) // Left mouse button is held down
        {
            float chargeDuration = Time.time - chargeStartTime;
            currentLaunchVelocity = Mathf.Clamp(minLaunchVelocity + (chargeDuration * stepAmplifier), minLaunchVelocity, maxLaunchVelocity);
        }

        if (Input.GetMouseButtonUp(0) && !isPaused) // Left mouse button released
        {
            isCharging = false;

            if (chargeBarUIController != null)
            {
                chargeBarUIController.StopCharging();
            }

            if (projectilePrefab != null && projectileSpawnPoint != null)
            {
                // Instantiate the projectile
                GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);

                // Get the Projectile script component and launch it
                Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.Launch(currentLaunchVelocity * velocityAmplifier);
                }
                else
                {
                    Debug.LogError("Projectile prefab does not have a Projectile script attached.");
                    Destroy(projectileInstance); // Clean up if script is missing
                }
            }
        }

        // Handle pause input
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TogglePause();
        // }
    }

    // void TogglePause()
    // {
    //     isPaused = !isPaused;

    //     if (pauseMenuUI != null)
    //     {
    //         pauseMenuUI.SetActive(isPaused);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Pause Menu UI GameObject not assigned in the Inspector.");
    //     }

    //     if (isPaused)
    //     {
    //         Time.timeScale = 0f; // Stop time
    //     }
    //     else
    //     {
    //         Time.timeScale = 1f; // Resume time
    //     }
    // }

    // // You might want to add a public method to resume from the pause menu button
    // public void ResumeGame()
    // {
    //     TogglePause();
    // }
}