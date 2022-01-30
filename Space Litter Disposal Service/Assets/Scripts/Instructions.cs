using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    //method for back button - loads main menu scene
    public void BackButton(){
        SceneManager.LoadScene(0);
    }
}
