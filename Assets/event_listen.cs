using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
public class event_listen : MonoBehaviour
{
    public string event_string;
    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEvents(event_string, OnEvent);
    }

    void OnEvent(KoreographyEvent evt){
        Debug.Log("event: " + event_string);
    }
}
