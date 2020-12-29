using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform target;
    public int bpm = 120;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        float lead = (bpm / 60);
        lead = lead / 4;
        StartCoroutine(MoveOverSeconds(this.gameObject, target.position, lead));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Hero.active.Damage(damage);
            Destroy(this.gameObject);
        }


    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }
}
