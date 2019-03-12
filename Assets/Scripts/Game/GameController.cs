using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int highscore;

    [Header("Health Settings")]
    public int maxHealth = 6;
    public int curHealth;

    [Header("Controllers")]
    public MenuController mc;
    public WaveController wc;

    private bool gameLost = false;

    private void Awake() 
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        if(curHealth <= 0 && gameLost == false)
        {
            gameLost = true;
            GameLost();
        }
    }
    
    private void GameLost()
    {
        LoadHighscore();
        if(wc.getWaveNumber() > highscore)
        {
            SaveHighscore();
        }
        gameLost = true;
        mc.FreezeGame();
        mc.ShowGameLost();
    }

    public void SaveHighscore()
    {
        print("Saving highscore.dat to: " + Application.persistentDataPath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.OpenOrCreate);

        Highscore hs = new Highscore();
        hs.highscore = wc.getWaveNumber();

        bf.Serialize(file, hs);
        file.Close();

        print("You have the highscore!");
    }

    public void LoadHighscore()
    {
        if(File.Exists(Application.persistentDataPath + "/highscore.dat"))
        {
            print("Loading highscore.dat from: " + Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.Open);

            Highscore hs = (Highscore)bf.Deserialize(file);

            file.Close();

            highscore = hs.highscore;
            print("Current Highscore: " + highscore);
        }
        else
        {
            print("highscore.dat not found...");
            SaveHighscore();
        }
    }

    [Serializable]
    class Highscore
    {
        public int highscore;
    }
}
