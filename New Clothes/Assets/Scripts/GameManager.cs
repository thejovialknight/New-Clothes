using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Save save = null;
    public string filename = "test";

    [Header("Scenes")]
    public string menuSceneName = "Menu";

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (save.filename == "")
        {
            LoadGame();
        }
    }

    void SaveFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SavePath(save.filename));
        bf.Serialize(file, save);
        file.Close();
    }

    void LoadFile()
    {
        if (File.Exists(SavePath(filename)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SavePath(filename), FileMode.Open);
            save = (Save)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            save = new Save(filename);
        }
    }

    string SavePath(string fName)
    {
        return Application.persistentDataPath + "/saves" + fName + ".sav";
    }

    public void SaveGame()
    {
        RaiseOnSave();
        save.filename = filename;
        SaveFile();
        RaiseOnPostSave();
    }

    public void LoadGame()
    {
        RaiseOnPreFileLoad();
        LoadFile();
        RaiseOnFileLoad();
    }

    public void EnterGame(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void EnterMenu()
    {
        SceneManager.LoadSceneAsync(menuSceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public delegate void OnTrigger();
    public static event OnTrigger onPreFileLoad;
    void RaiseOnPreFileLoad()
    {
        if (onPreFileLoad != null)
        {
            onPreFileLoad();
        }
    }

    public static event OnTrigger onFileLoad;
    void RaiseOnFileLoad()
    {
        if (onFileLoad != null)
        {
            onFileLoad();
        }
    }

    public static event OnTrigger onSave;
    void RaiseOnSave()
    {
        if (onSave != null)
        {
            onSave();
        }
    }

    public static event OnTrigger onPostSave;
    void RaiseOnPostSave()
    {
        if (onPostSave != null)
        {
            onPostSave();
        }
    }
}