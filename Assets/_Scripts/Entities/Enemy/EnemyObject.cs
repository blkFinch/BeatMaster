using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Enemy")]
public class EnemyObject : ScriptableObject
{
   public float maxHealth = 10f;
   public Sprite sprite;
   public string name;
}
