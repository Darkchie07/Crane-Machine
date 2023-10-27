using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    public static int coin = 5;
    public TMP_Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 5);
            PlayerPrefs.Save();
        }

        coin = PlayerPrefs.GetInt("Coins");
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinText.text = coin.ToString();
    }

    public void AddCoins(int amount)
    {
        coin += amount;
        PlayerPrefs.SetInt("Coins", coin);
        PlayerPrefs.Save();
        UpdateCoinText();
    }
}
