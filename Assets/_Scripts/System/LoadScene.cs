using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;

    public void OnBlue(){
        Debug.Log("button press");
        SceneManager.LoadScene(sceneToLoad);
    }
}
