using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleOnLocation : MonoBehaviour
{
    public Color thisColor;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 1f);
    }

}
