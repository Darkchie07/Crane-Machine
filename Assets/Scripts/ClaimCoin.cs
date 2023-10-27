using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClaimCoin : MonoBehaviour
{
    public Button button;
    public Currency currency;
    public TMP_Text cooldownText;
    public float cooldownDuration = 86400f; // 1 day in seconds
    private float cooldownEndTime;

    void Start()
    {
        // Retrieve the cooldown end time from PlayerPrefs
        cooldownEndTime = PlayerPrefs.GetFloat("CooldownEndTime", 0);

        // Check if the cooldown has already ended
        if (Time.time >= cooldownEndTime)
        {
            button.interactable = true;
            cooldownText.text = "Claim Coin";
        }
        else
        {
            button.interactable = false;
            UpdateCooldownText();
        }
    }

    void Update()
    {
        if (!button.interactable)
        {
            if (Time.time >= cooldownEndTime)
            {
                button.interactable = true;
                cooldownText.text = "Claim Coin";
            }
            else
            {
                UpdateCooldownText();
            }
        }
    }

    public void OnButtonClick()
    {
        if (button.interactable)
        {
            currency.AddCoins(Random.Range(1, 4));
            currency.UpdateCoinText();
            
            cooldownEndTime = Time.time + cooldownDuration;
            button.interactable = false;
            UpdateCooldownText();

            
            PlayerPrefs.SetFloat("CooldownEndTime", cooldownEndTime);
            PlayerPrefs.Save();
        }
    }

    private void UpdateCooldownText()
    {
        float remainingTime = cooldownEndTime - Time.time;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        cooldownText.text = string.Format("Cooldown: {0:00}:{1:00}", minutes, seconds);
    }
}
