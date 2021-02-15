using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    Text text;
    [Range(0.0f, 3f)]
    public float blinkOnTime = 1f;
    [Range(0.0f, 3f)]
    public float blinkOffTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(Blink());  
    }

    IEnumerator Blink(){
        text.enabled = true;
        yield return new WaitForSeconds(blinkOnTime);
        text.enabled = false;
        yield return new WaitForSeconds(blinkOffTime);
        StartCoroutine(Blink());
    }
}
