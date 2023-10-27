using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlay : MonoBehaviour
{
    public Currency currency;
    public GameObject panelHome;
    public GameObject panelController;
    public GameObject panelEnterCoin;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RemoveCoins(1);
        }
    }

    public void RemoveCoins(int amount)
    {
        if (Currency.coin >= amount)
        {
            Currency.coin -= amount;
            PlayerPrefs.SetInt("Coins", Currency.coin);
            PlayerPrefs.Save();
            currency.UpdateCoinText();
            panelController.SetActive(true);
            panelEnterCoin.gameObject.SetActive(false);
        }
        else
        {
            panelHome.SetActive(true);
        }
    }
}
