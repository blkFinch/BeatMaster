using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEvents("snare_hit", onSnareHit);
    }

    void onSnareHit(KoreographyEvent evt){
       GameObject note = Instantiate(notePrefab, this.transform.position, Quaternion.identity);
       note.GetComponent<Target>().target = this.target;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
