using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUp : MonoBehaviour
{
    private Text text;
    public int secondsToDisplay = 3;
    public static PopUp active;

    void Awake() {
        if(active != null){
            Destroy(this.gameObject);
        }else{
            active = this;
        }
        text = GetComponent<Text>();
        text.text = "";
    }
    
    public void DisplayText(string text){
        StartCoroutine(DisplayTextRoutine(text));
    }

    IEnumerator DisplayTextRoutine(string text){
        this.text.text = text;
        yield return new WaitForSeconds(secondsToDisplay);
        this.text.text = "";
    }
}
