using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPill : MonoBehaviour
{
    public int health = 10;
    public AudioClip sound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Hero.active.Heal(health);
            SoundEffectManager.active.PlayClip(sound);
            Destroy(this.gameObject);
        }
    }
}
