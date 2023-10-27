using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("ClawMachine");
    }

    public void Home()
    {
        Debug.Log("play");
        SceneManager.LoadScene("Home");
    }
    public void Quit()
    {
        Application.Quit();
    } 
}
