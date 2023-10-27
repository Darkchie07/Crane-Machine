using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizePool : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject controlPanel;
    public Transform parentPrize;
    public List<GameObject> AllPrize = new List<GameObject>();
    public bool hasCalled;

    private void Start()
    {
        hasCalled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Masuk");
        if (other.gameObject.tag == "Prize")
        {
            if (!AllPrize.Contains(other.gameObject))
            {
                AllPrize.Add(other.gameObject);
            }
            if (!hasCalled)
            {
                Invoke("ShowPrize", 3f);
                hasCalled = true;
            }
        }
    }

    private void ShowPrize()
    {
        for (int i = 0; i < AllPrize.Count; i++)
        {
            Debug.Log(i);
            GameObject winObject = Instantiate(AllPrize[i], parentPrize);
            winObject.transform.localPosition = new Vector3(0, -100, 0);
            winObject.transform.rotation = Quaternion.identity;
            winObject.layer = 5;
            Destroy(winObject.GetComponent<Rigidbody>());
            winObject.transform.localScale = new Vector3(1000, 1000, 1000);
            if (i == 0)
            {
                winObject.SetActive(true);
            }
            else
            {
                winObject.SetActive(false);
            }
        }
        WinPanel.SetActive(true);
        controlPanel.SetActive(false);
        foreach(GameObject i in AllPrize)
        {
            Destroy(i);
        }
    }

    public void ClosePrizePanel()
    {
        hasCalled = false;
    }
}
