using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;
public class BarCounter : MonoBehaviour
{
    public static int barNumber;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Koreographer.Instance.RegisterForEvents("master_bar_count", onNewBar);
    }

    void onNewBar(KoreographyEvent evt){
        int barNum = evt.GetIntValue();
        text.text = "BAR: " + barNum;
    }
}
