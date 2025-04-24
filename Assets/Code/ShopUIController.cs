using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject openShopButton;

    void Start()
    {
        // Ensure the OpenShopButton is hidden when in the Shop scene
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            openShopButton.SetActive(false);
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f;

        // Hide the OpenShopButton when the shop is open
        openShopButton.SetActive(false);

        FindAnyObjectByType<ShopManager>()?.UpdateUI();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;

        // Show the OpenShopButton when the shop is closed
        openShopButton.SetActive(true);
    }
}