using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager active;
    private Enemy activeCombatEnemy;
    private List<Enemy> activeEnemies;

    private Enemy redEnemy;
    private Enemy blueEnemy;
    private Enemy yellowEnemy;

    public delegate void OnEnemyRegisteredDelegate();
    public static OnEnemyRegisteredDelegate enemyRegisteredDelegate;

    void Awake() {
        if(active == null){
            active = this;
        }else{
            Destroy(this.gameObject);
        }
        activeEnemies = new List<Enemy>();
    }

    public void RegisterCombatEnemy(Enemy enemy){
        Debug.Log("Registering enemy: " + enemy);
        switch(enemy.type){
            case AttackType.BLUE:
                blueEnemy = enemy;
                break;
            case AttackType.RED:
                redEnemy = enemy;
                break;
            case AttackType.YELLOW:
                yellowEnemy = enemy;
                break;
            default:
                activeCombatEnemy = enemy;
                break;
        }
        activeEnemies.Add(enemy);
        enemyRegisteredDelegate();
    }

    public void DeregisterEnemy(Enemy enemy){
        activeEnemies.Remove(enemy);
        if(activeEnemies.Count <= 0){
            Hero.active.ExitCombat();
        }
    }

    public Enemy GetActiveEnemy(){
        return activeCombatEnemy;
    }

     public Enemy GetRedEnemy(){
        return redEnemy;
    }

    public Enemy GetBlueEnemy(){
        return blueEnemy;
    }

    public Enemy GetYellowEnemy(){
        return yellowEnemy;
    }
}
