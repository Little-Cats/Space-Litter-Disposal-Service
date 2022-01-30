using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //method for play button - loads game scene
    public void PlayButton(){
        SceneManager.LoadScene(2);
    }

    //method for instructions button - loads instructions scene
    public void InstButton(){
        SceneManager.LoadScene(1);
    }

    //method for quit button - exits application
    public void QuitButton(){
        Application.Quit();
    }
}
