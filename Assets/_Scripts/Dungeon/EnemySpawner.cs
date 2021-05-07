using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, this.transform.position, Quaternion.identity);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, 1f);
    }

}
