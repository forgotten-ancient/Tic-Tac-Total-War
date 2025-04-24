using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI soldiersText;
    public int soldierCost = 25;
    public GameObject soldierPrefab;
    public Transform spawnPoint;

    void Start()
    {
        UpdateUI();
    }
//when someone buys a soldier, this will pull the update from the GameManager script and will update in the text and display coins  
    public void BuySoldier()
    {
        if (GameManager.instance.SpendCoins(soldierCost))
        {
            GameManager.instance.AddSoldier();
            UpdateUI();
            // if (soldierPrefab != null && spawnPoint != null)
            // {
            //     Instantiate(soldierPrefab, spawnPoint.position, spawnPoint.rotation);
            // }
        }
        else
        {
            Debug.Log("Not enough coins to buy a soldier!");
        }
    }
//updates the text - amount of coins, amount of soldiers bought/in inventory
    public void UpdateUI()
    {
        coinsText.text = "Coins: " + GameManager.instance.coins.ToString();
        soldiersText.text = "Soldiers: " + GameManager.instance.currentSoldiers.ToString();
        Debug.Log("UI updated: Coins = " + GameManager.instance.coins + ", Soldiers = " + GameManager.instance.currentSoldiers);
    }
}