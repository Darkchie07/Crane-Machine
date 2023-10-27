using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject prize;
    public float rotationSpeed;
    public List<GameObject> childObjectsList = new List<GameObject>();
    public int index;
    public TMP_Text prizeName;
    public GameObject next, before;

    void Update()
    {
        prize.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        foreach (Transform child in prize.transform)
        {
            childObjectsList.Add(child.gameObject);
        }
        index = 0;
        childObjectsList[index].SetActive(true);
        string[] splitArray = childObjectsList[index].name.Split('(');
        prizeName.text = splitArray[0];
        if(childObjectsList.Count == 1)
        {
            next.SetActive(false);
            before.SetActive(false);
        }
    }

    public void Next()
    {
        index += 1;
        if (index >= childObjectsList.Count)
        {
            index = 0;
        }
        resetChild();
        childObjectsList[index].SetActive(true);
        string[] splitArray = childObjectsList[index].name.Split('(');
        prizeName.text = splitArray[0];
    }

    public void Before()
    {
        index -= 1;
        if (index <= -1)
        {
            index = childObjectsList.Count - 1;
        }
        resetChild();
        childObjectsList[index].SetActive(true);
        string[] splitArray = childObjectsList[index].name.Split('(');
        prizeName.text = splitArray[0];
    }

    public void resetChild()
    {
        foreach(GameObject i in childObjectsList)
        {
            i.SetActive(false);
        }
    }
}
