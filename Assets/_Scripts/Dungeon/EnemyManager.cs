using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager active;
    private Enemy activeCombatEnemy;

    void Awake() {
        if(active == null){
            active = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void RegisterCombatEnemy(Enemy enemy){
        activeCombatEnemy = enemy;
    }

    public Enemy GetActiveEnemy(){
        return activeCombatEnemy;
    }
}
