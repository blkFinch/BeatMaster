using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public List<Enemy> enemies;

    public static EnemyFormation active;

    void Awake()
    {
        //init singleton
        if (active == null) { active = this; }
        else { Destroy(this.gameObject); }


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count <= 0){
            DestructFormation();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("combat entered");
            Hero.active.EnterCombat();
        }
    }

    public void DestructEnemy(Enemy enemy){
        if(enemies.Remove(enemy)){
            enemy.Kill();
        }
    }

    //Deregister and end combat
    private void DestructFormation()
    {
        Hero.active.ExitCombat();
        Destroy(this.gameObject);
    }
}
