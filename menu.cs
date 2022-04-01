using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public Scene main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void freeplay()
    {
        GameObject.Find("Settings").GetComponent<Settings>().Game_mode = 1;
        SceneManager.LoadScene(1);
        
    }
    public void Arcade_Mode()
    {
        
        GameObject.Find("Settings").GetComponent<Settings>().Game_mode = 2;
        SceneManager.LoadScene(1);
    }

}
