using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public string spawnEventTag;
    private int count = 10;
    // Start is called before the first frame update
    void Start()
    {
      Koreographer.Instance.RegisterForEvents(spawnEventTag, SpawnCube);  
    }

    void SpawnCube(KoreographyEvent evt){
        count--;
        if(count > 0)
            Instantiate(cube, this.transform.position,  Quaternion.Euler(new Vector3(30, 30, 0)));
    }


}
