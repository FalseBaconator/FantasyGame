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
    /*public enum Dungeon { GoblinCavern, };
    public Dungeon currentDungeon;*/

    public TextMeshProUGUI XPText;

    public MapGenerator mapGenerator;

    public enum GameState { MainMenu, Saves, Options, Upgrades, Map, Combat, Pause, Lose, Win, BetweenDungeons };
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
                    combatManager.playing = true;
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
                case GameState.BetweenDungeons:
                    Time.timeScale = 1;
                    uiManager.OpenBetween();
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
            case GameState.BetweenDungeons: break;
        }
    }

    //Delays gamestate change until after the scene changed to prevent code being missed
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
            case GameState.BetweenDungeons:
            case GameState.Lose:
                gameState = GameState.Upgrades;
                break;
            case GameState.Pause:
            case GameState.Win:
                gameState = GameState.MainMenu;
                break;
        }
    }

    //On Button Press Methods. Each changes scene
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

    //On Button Press. Saves chosen upgrades, the tells MapGenerator to start the dungeon.
    public void StartNewDungeon()
    {
        SaveGame();
        gameState = GameState.Map;
        mapGenerator.NewAttempt();
    }

    //Return to Map from Combat. Prepares the next stage of rooms.
    public void GoToMap()
    {
        gameState = GameState.Map;
        mapGenerator.GenerateNextEncounter();
    }

    //Change Game State methods without changing scene.
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
        //Checks if it's the end of the game or not
        if (mapGenerator.currentMap < mapGenerator.maps.Length - 1)
        {
            mapGenerator.currentMap++;
            GoToBetween();
        }
        else {
            SaveGame();
            gameState = GameState.Win;
        }
    }

    public void GoToBetween()
    {
        gameState = GameState.BetweenDungeons;
    }

    //Gets data from save file and distributes it accross the game as needed.
    public void LoadSave(int saveIndex)
    {
        //Saves which save file it is
        currentSaveIndex = saveIndex;
        if (newGame)
        {
            //Doesn't load data, instead uses default starting data.
            XP = 100;   //Would be 0 for actual game. Is 100 for testing purposes.
            mapGenerator.currentMap = 0;
            upgradeManager.returnToDefault();
        }
        else
        {
            //Gets Data from the appropriate data
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
                        //Distributes data accross GameManager and Upgrade Manager
                        data.GetDataFromFile(this, upgradeManager);
                    }
                    else
                    {
                        //Loads a new game if the save data was empty
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
                        //Distributes data accross GameManager and Upgrade Manager
                        data.GetDataFromFile(this, upgradeManager);
                    }
                    else
                    {
                        //Loads a new game if the save data was empty
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
                        //Distributes data accross GameManager and Upgrade Manager
                        data.GetDataFromFile(this, upgradeManager);
                    }
                    else
                    {
                        //Loads a new game if the save data was empty
                        newGame = true;
                        LoadSave(saveIndex);
                    }
                    break;
            }
        }
        GoToUpgrades();
    }

    //On Button Press. Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }

    //On Certain State Changes. Saves Game to most recently opened save file.
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
        //Gets data from Game Manager and Upgrade Manager.
        data.GetDataFromGame(this, upgradeManager);
        //Saves data to file.
        bf.Serialize(file, data);
        file.Close();
    }

}
