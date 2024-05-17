using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void LoadFirstLevel(){
        SceneManager.LoadScene("GameScene");
    }

    void Update()
    {
        
    }

    public void CloseGame(){
        Application.Quit();
    }
}
