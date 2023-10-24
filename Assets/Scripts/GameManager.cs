using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int mainMenuScene;
    public int upgradesScene;
    public int gamePlayScene;

    public UIManager uiManager;
    public CombatManager combatManager;
    public UpgradeManager upgradeManager;

    private bool newGame;

    public int currentSaveIndex;

    public int XP;
    public enum Dungeon { GoblinCavern, };
    public Dungeon currentDungeon;

    public TextMeshProUGUI XPText;

    public MapGenerator mapGenerator;

    public enum GameState { MainMenu, Saves, Options, Upgrades, Map, Combat, Pause, Lose, Win };
    private GameState _gState = GameState.MainMenu;
    private GameState prevState;
    public GameState gameState{
        get => _gState;
        set
        {
            prevState = _gState;
            switch (value)
            {
                case GameState.MainMenu:
                    Time.timeScale = 1;
                    uiManager.OpenMainMenu();
                    //SaveGame();
                    break;
                case GameState.Saves:
                    Time.timeScale = 1;
                    uiManager.OpenSavesScreen();
                    break;
                case GameState.Options:
                    Time.timeScale = 1;
                    uiManager.OpenOptions();
                    break;
                case GameState.Upgrades:
                    Time.timeScale = 1;
                    uiManager.OpenUpgrades();
                    SaveGame();
                    break;
                case GameState.Map:
                    Time.timeScale = 1;
                    uiManager.OpenMap();
                    break;
                case GameState.Combat:
                    Time.timeScale = 1;
                    uiManager.OpenCombat();
                    //combatManager.StartCombat();
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    uiManager.OpenPauseScreen();
                    break;
                case GameState.Lose:
                    Time.timeScale = 1;
                    uiManager.OpenLose();
                    break;
                case GameState.Win:
                    Time.timeScale = 1;
                    uiManager.OpenWin();
                    break;
            }
            _gState = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Saves:
                break;
            case GameState.Options:
                break;
            case GameState.Upgrades:
                XPText.text = "XP: " + XP;
                break;
            case GameState.Map:
                break;
            case GameState.Combat:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    exitPause();
                }
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
        }
    }

    void SwitchScreenSceneTransition(Scene scene, LoadSceneMode mode)
    {
        switch (gameState)
        {
            case GameState.MainMenu: break;
            case GameState.Options: break;
            case GameState.Upgrades: break;
            case GameState.Map:
                gameState = GameState.Combat;
                break;
            case GameState.Combat: break;
            case GameState.Saves:
            case GameState.Lose:
                gameState = GameState.Upgrades;
                break;
            case GameState.Pause:
            case GameState.Win:
                gameState = GameState.MainMenu;
                break;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        SceneManager.sceneLoaded += SwitchScreenSceneTransition;
    }

    public void GoToUpgrades()
    {
        SceneManager.LoadScene(upgradesScene);
        SceneManager.sceneLoaded += SwitchScreenSceneTransition;
    }

    public void GoToCombat()
    {
        SceneManager.LoadScene(gamePlayScene);
        SceneManager.sceneLoaded += SwitchScreenSceneTransition;
    }

    public void StartNewDungeon()   //Should have more code to make CombatManager make the dungeon
    {
        gameState = GameState.Map;
        mapGenerator.NewAttempt();
    }

    public void GoToMap()
    {
        gameState = GameState.Map;
        mapGenerator.GenerateNextEncounter();
    }

    public void goToOptions()
    {
        gameState = GameState.Options;
    }

    public void goToPrev()
    {
        gameState = prevState;
    }

    public void goToSave(bool newGame)
    {
        this.newGame = newGame;
        gameState = GameState.Saves;
    }

    public void exitPause()
    {
        gameState = GameState.Combat;
    }

    public void Lose()
    {
        gameState = GameState.Lose;
    }

    public void Win()
    {
        gameState = GameState.Win;
    }

    public void LoadSave(int saveIndex)
    {
        currentSaveIndex = saveIndex;
        if (newGame)
        {
            XP = 100;
            currentDungeon = Dungeon.GoblinCavern;
            upgradeManager.returnToDefault();
        }
        else
        {
            switch (currentSaveIndex)
            {
                case 1:
                    if(File.Exists(Application.persistentDataPath + "/Save1.dat"))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + "/Save1.dat", FileMode.Open);
                        SaveData data = (SaveData)bf.Deserialize(file);
                        foreach (int stage in data.stages)
                        {
                            Debug.Log(stage);
                        }
                        file.Close();
                        data.GetDataFromFile(this, upgradeManager);
                    }
                    else
                    {
                        newGame = true;
                        LoadSave(saveIndex);
                    }
                    break;
                case 2:
                    if (File.Exists(Application.persistentDataPath + "/Save2.dat"))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + "/Save2.dat", FileMode.Open);
                        SaveData data = (SaveData)bf.Deserialize(file);
                        foreach (int stage in data.stages)
                        {
                            Debug.Log(stage);
                        }
                        file.Close();
                        data.GetDataFromFile(this, upgradeManager);
                    }
                    else
                    {
                        newGame = true;
                        LoadSave(saveIndex);
                    }
                    break;
                case 3:
                    if (File.Exists(Application.persistentDataPath + "/Save3.dat"))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + "/Save3.dat", FileMode.Open);
                        SaveData data = (SaveData)bf.Deserialize(file);
                        foreach(int stage in data.stages)
                        {
                            Debug.Log(stage);
                        }
                        file.Close();
                        data.GetDataFromFile(this, upgradeManager);
                    }
                    else
                    {
                        newGame = true;
                        LoadSave(saveIndex);
                    }
                    break;
            }
        }
        GoToUpgrades();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        switch (currentSaveIndex)
        {
            default:
            case 1:
                file = File.Create(Application.persistentDataPath + "/Save1.dat");
                break;
            case 2:
                file = File.Create(Application.persistentDataPath + "/Save2.dat");
                break;
            case 3:
                file = File.Create(Application.persistentDataPath + "/Save3.dat");
                break;
        }
        SaveData data = new SaveData();
        data.GetDataFromGame(this, upgradeManager);
        bf.Serialize(file, data);
        file.Close();
        foreach (int stage in data.stages)
        {
            Debug.Log(stage);
        }
    }

}
