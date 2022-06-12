using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    private void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
        PlayerDeath.highScore = loadedPlayerData.highScore;
    }
    
    public class PlayerData
    {
        public int highScore;
    }
}
