using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    public float newSize;
    public float oldSize = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = newSize;
    }

    void OnDestroy() {
        Camera.main.orthographicSize = oldSize;
    }
}
