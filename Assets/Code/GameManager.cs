using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coins = 500;
    public int currentSoldiers = 0;
    [SerializeField] Canvas canvas;


    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);


            coins = PlayerPrefs.GetInt("Coins", 500);
            currentSoldiers = PlayerPrefs.GetInt("Soldiers", 0);
        }
        else
        {
            Destroy(gameObject);
        }
        ResetProgress();
        canvas.enabled = false;
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt("Coins", coins);
            return true;
        }
        return false;
    }

    public void AddSoldier()
    {
        currentSoldiers++;
        PlayerPrefs.SetInt("Soldiers", currentSoldiers);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Soldiers", currentSoldiers);
    }

    public void ResetProgress()
    {
        coins = 500;
        currentSoldiers = 0;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Soldiers", currentSoldiers);
    }
}
