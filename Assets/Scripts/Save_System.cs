using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SHeroData
{
    public Controller_Hero ControllerHero;
}


[System.Serializable]
public class SHeroProperties
{
    public int HeroID;
    public string HeroName;
    public int Health;
    public int AttackPower;
    public int Experience;
    public int Level;
    public bool IsOwnedByPlayer;
    public SHeroProperties(int pHeroID, string pHeroName, int pHealth, int pAttackPower, int pExperience, int pLevel, bool pIsOwnedByPlayer)
    {
        this.HeroID = pHeroID;
        this.HeroName = pHeroName;
        this.Health = pHealth;
        this.AttackPower = pAttackPower;
        this.Experience = pExperience;
        this.Level = pLevel;
        this.IsOwnedByPlayer = pIsOwnedByPlayer;
    }
}

[System.Serializable]
public class SaveData
{
    public int DefaultExperience = 0;
    public int DefaultLevel = 0;
    public bool DefaultIsOwnedByPlayer = false;
    public List<SHeroProperties> listHeroProperties;
    public bool isHeroPropertiesLoaded = false;

    public SaveData()
    {
        listHeroProperties = new List<SHeroProperties>();
    }

    public void ResetAllData()
    {
        ResetAllHeroAttributes();
        listHeroProperties.Clear();
        isHeroPropertiesLoaded = false;
    }

    public void ResetAllHeroAttributes()
    {
        foreach(SHeroProperties heroProperties in listHeroProperties)
        {
            heroProperties.Experience = this.DefaultExperience;
            heroProperties.Level = this.DefaultLevel;
            heroProperties.IsOwnedByPlayer = this.DefaultIsOwnedByPlayer;
        }
    }
    
};
public class Save_System : MonoBehaviour
{
    private const string DATANAME = "SaveData.json";
    // private const string DATANAME = "SaveData.mcdata";
    public static SaveData Data = new SaveData();

    public static void Save()
    {
        string filePath = Application.persistentDataPath + "/" + DATANAME;
        
        string rawJson = JsonUtility.ToJson(Data);
        System.IO.File.WriteAllText(filePath, rawJson);
        Debug.Log("Game Saved");
    }
    public static void Load()
    {
        string filePath = Application.persistentDataPath + "/" + DATANAME;
        if(File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);
            Data = JsonUtility.FromJson<SaveData>(fileContents);   
            Debug.Log("Game Loaded");
        }
    }
}
