using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizePool : MonoBehaviour
{
    public GameObject WinPanel;
    public Transform parentPrize;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Masuk");
        if (other.gameObject.tag == "Prize")
        {
            WinPanel.SetActive(true);
            GameObject winObject = Instantiate(other.gameObject, parentPrize);
            winObject.transform.localPosition = new Vector3(0, -100, 0);
            winObject.layer = 5;
            Destroy(winObject.GetComponent<Rigidbody>());
            winObject.transform.localScale = new Vector3(1000, 1000, 1000);
        }
    }
}
