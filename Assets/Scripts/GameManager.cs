using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int mainMenuScene;
    public int upgradesScene;
    public int gamePlayScene;

    public UIManager uiManager;

    private bool newGame;

    public int currentSaveIndex;

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
                    Debug.Log("4");
                    Time.timeScale = 1;
                    uiManager.OpenMainMenu();
                    SaveGame(currentSaveIndex);
                    break;
                case GameState.Saves:
                    Time.timeScale = 1;
                    uiManager.OpenSavesScreen();
                    break;
                case GameState.Options:
                    Time.timeScale = 0;
                    uiManager.OpenOptions();
                    break;
                case GameState.Upgrades:
                    Time.timeScale = 1;
                    uiManager.OpenUpgrades();
                    SaveGame(currentSaveIndex);
                    break;
                case GameState.Map:
                    Time.timeScale = 1;
                    uiManager.OpenMap();
                    break;
                case GameState.Combat:
                    Time.timeScale = 1;
                    uiManager.OpenCombat();
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    uiManager.OpenPauseScreen();
                    break;
                case GameState.Lose:
                    Time.timeScale = 0;
                    uiManager.OpenLose();
                    break;
                case GameState.Win:
                    Time.timeScale = 0;
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
        Debug.Log("1");
        SceneManager.LoadScene(mainMenuScene);
        Debug.Log("2");
        SceneManager.sceneLoaded += SwitchScreenSceneTransition;
        //gameState = GameState.MainMenu;
        Debug.Log("3");
    }

    public void GoToUpgrades()
    {
        SceneManager.LoadScene(upgradesScene);
        SceneManager.sceneLoaded += SwitchScreenSceneTransition;
        //gameState = GameState.Upgrades;
    }

    public void GoToCombat(int scenarioIndex)
    {
        SceneManager.LoadScene(gamePlayScene);
        SceneManager.sceneLoaded += SwitchScreenSceneTransition;
        //gameState = GameState.Combat;
    }

    public void StartNewDungeon()   //Should have more code to make CombatManager make the dungeon
    {
        //SceneManager.LoadScene(gamePlayScene);
        Debug.Log("A");
        gameState = GameState.Map;
    }

    public void GoToMap()
    {
        gameState = GameState.Map;
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

        }
        else
        {

        }
        GoToUpgrades();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame(int saveFile = -1)
    {

    }

}
