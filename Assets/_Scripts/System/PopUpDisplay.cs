using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDisplay : MonoBehaviour
{
    public string displayString;
    // Start is called before the first frame update
    void Start()
    {
        PopUp.active.DisplayText(displayString);
    }
}
