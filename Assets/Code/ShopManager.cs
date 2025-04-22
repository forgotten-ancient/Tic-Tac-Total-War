using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI soldiersText;
    public int soldierCost = 25;
    public GameObject soldierPrefab;
    public Transform spawnPoint;
    public GameObject openShopButton;

    public GameObject shopPanel;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUI();  
    }

    void Start()
    {
        // Hide the shop panel on start
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        UpdateUI(); 
    }

    public void BuySoldier()
    {
        Debug.Log("BuySoldier called!");

     
        if (GameManager.instance.SpendCoins(soldierCost))
        {
            Debug.Log("Soldier bought!");
            GameManager.instance.AddSoldier();
            UpdateUI();  
        }
        else
        {
            Debug.Log("Not enough coins to buy a soldier!");
        }
    }

    public void UpdateUI()
    {
        Debug.Log("UpdateUI CALLED");

        if (coinsText == null)
            Debug.LogError("coinsText is NULL");
        if (soldiersText == null)
            Debug.LogError("soldiersText is NULL");

        if (coinsText != null && soldiersText != null)
        {
           
            coinsText.text = "Coins: " + GameManager.instance.coins;
            soldiersText.text = "Soldiers: " + GameManager.instance.currentSoldiers;
            Debug.Log("Updated UI: Coins = " + GameManager.instance.coins + ", Soldiers = " + GameManager.instance.currentSoldiers);
        }
    }

    public void ToggleShop()
    {
        if (shopPanel != null)
        {
            bool isActive = !shopPanel.activeSelf;
            shopPanel.SetActive(isActive);

            // Toggle the OpenShopButton visibility 
            if (openShopButton != null)
            {
                openShopButton.SetActive(!isActive); 
            }

            if (isActive)
            {
                UpdateUI();  
            }
        }
    }
}