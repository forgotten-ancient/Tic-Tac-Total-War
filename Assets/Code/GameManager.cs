using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coins = 500; 
    public int currentSoldiers = 1; 

  
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI soldiersText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);


            coins = PlayerPrefs.GetInt("Coins", 500); 
            currentSoldiers = PlayerPrefs.GetInt("Soldiers", 1);

            Debug.Log("GameManager Initialized: Coins = " + coins + ", Soldiers = " + currentSoldiers);  
        }
        else
        {
            Destroy(gameObject); 
        }


        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            Debug.Log("Coins after spending: " + coins);
            PlayerPrefs.SetInt("Coins", coins); 
            UpdateUI(); 
            return true;
        }
        return false;
    }

    public void AddSoldier()
    {
        currentSoldiers++;
        Debug.Log("Soldiers after adding: " + currentSoldiers);
        PlayerPrefs.SetInt("Soldiers", currentSoldiers); 
        UpdateUI(); 
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Soldiers", currentSoldiers);
    }

    public void ResetProgress()
    {
        coins = 500;
        currentSoldiers = 1;
        SaveProgress();
        UpdateUI();  
    }

    // update the UI txt
    private void UpdateUI()
    {
        if (coinsText != null)
            coinsText.text = "Coins: " + coins.ToString();

        if (soldiersText != null)
            soldiersText.text = "Soldiers: " + currentSoldiers.ToString();
    }
}