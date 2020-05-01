using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class StartGame : MonoBehaviour
{
    public string StartScene = "IntroText";
    string SceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        string strg = Application.persistentDataPath + "\\" + "FlagData.txt";
		string path = @strg;

        if (!File.Exists(path))
        {
            File.CreateText(path).Dispose();
        }

        if (PlayerPrefs.GetInt("ResetGame", 1) == 1)
        {
           ResetGame();
        }


        string[] allFlags = File.ReadAllLines(path);
        foreach (string flag in allFlags)
        {
            GameStateTracker.GameFlags.Add(flag);
        }

        
        SceneToLoad = PlayerPrefs.GetString("CurrentScene");

        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
    }

    public void ResetGame()
    {
        string strg = Application.persistentDataPath + "\\" + "FlagData.txt";
		string path = @strg;

        if (File.Exists(path)) 
        {
            File.Delete(path); 
        }
        File.CreateText(path).Dispose();

        GameStateTracker.GameFlags = new HashSet<string>();

        PlayerPrefs.SetString("CurrentScene", StartScene);
        PlayerPrefs.SetFloat("X_Pos", 7.0f);
        PlayerPrefs.SetFloat("Y_Pos", -5.2f);
        PlayerPrefs.SetFloat("Z_Pos", 0.0f);

        PlayerPrefs.SetInt("ResetGame", 0);
    }
}
