using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    //method for play again button - loads game scene
    public void PlayAgainButton(){
        SceneManager.LoadScene(2);
    }

    //method for quit button - exits application
    public void QuitButton(){
        Application.Quit();
    }
}
