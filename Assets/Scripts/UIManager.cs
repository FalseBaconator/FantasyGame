using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject SavesScreen;
    public GameObject UpgradeScreen;
    public GameObject LevelSelectScreen;
    public GameObject DungeonScreen;
    public GameObject CombatScreen;
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public GameObject BetweenScreen;

    //Each of these methods makes it so that a single screen is shown at a time
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenSavesScreen()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(true);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenUpgrades()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(true);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(true);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenDungeon()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(true);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenCombat()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(true);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenPauseScreen()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(true);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(true);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenLose()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(true);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(false);
    }

    public void OpenWin()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(true);
        BetweenScreen.SetActive(false);
    }

    public void OpenBetween()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        DungeonScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        BetweenScreen.SetActive(true);
    }

}
