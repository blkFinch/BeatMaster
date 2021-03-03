using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    int atk =5; 
    int hp = 35;
    int xp, level;

    public int Atk { get => atk; set => atk = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Xp { get => xp; set => xp = value; }
    public int Level { get => level; set => level = value; }

}
