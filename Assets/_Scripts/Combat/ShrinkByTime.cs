using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkByTime : MonoBehaviour
{
    public Vector3 targetScale;
    // public int bpm = 120;
    public bool destroyAfterShrink = true;
    // Start is called before the first frame update
    void Start()
    {
        // float lead = (bpm/60);
        float lead = SongInfo.active.secondsPerBeat;
        StartCoroutine(ShrinkOverSeconds(targetScale, lead));
    }


    public IEnumerator ShrinkOverSeconds ( Vector3 endScale, float seconds)
 {
     float elapsedTime = 0;
     Vector3 startingScale = this.gameObject.transform.localScale;

     while (elapsedTime < seconds)
     {
         this.gameObject.transform.localScale = Vector3.Lerp(startingScale, endScale, (elapsedTime / seconds));
         elapsedTime += Time.deltaTime;
         yield return new WaitForEndOfFrame();
     }
     this.gameObject.transform.localScale = endScale;
     
     if(destroyAfterShrink){Destroy(this.gameObject);}
 }
}
