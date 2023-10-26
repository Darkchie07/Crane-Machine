using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject prize;
    public float rotationSpeed;
    
    void Update()
    {
        prize.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
