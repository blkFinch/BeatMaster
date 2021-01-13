using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad 
{
    public static void SaveHero(){
        //serializes HeroData to local hard disk
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/heroData.gd");
        bf.Serialize(file, Hero.active.stats);
        file.Close();
        Debug.Log("hero saved!");
    }   

    public static void LoadHero()
    {
        if (File.Exists(Application.persistentDataPath + "/heroData.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/heroData.gd", FileMode.Open);
            PlayerStats loadedHero = (PlayerStats)bf.Deserialize(file);
            file.Close();

            Hero.active.stats = loadedHero;

        }

    }
}
