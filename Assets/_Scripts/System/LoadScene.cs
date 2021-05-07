using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;

    public void OnBlue(){
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void OnRed(){
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnYellow(){
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnGreen(){
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnBlock(){
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void OnAction(){
        SceneManager.LoadScene(sceneToLoad);
    }
}
