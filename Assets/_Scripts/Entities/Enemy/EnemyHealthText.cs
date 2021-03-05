using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthText : MonoBehaviour
{
    private TextMeshPro text;

    void Awake() {
        text = this.GetComponent<TextMeshPro>();
    }
   public void UpdateHpDisplay(float hp){
       text.text = hp.ToString();
   }
}
