using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject SavesScreen;
    public GameObject UpgradeScreen;
    public GameObject MapScreen;
    public GameObject CombatScreen;
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject LoseScreen;
    public GameObject WinScreen;

    public void OpenMainMenu()
    {
        Debug.Log("5");
        MainMenu.SetActive(true);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenSavesScreen()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(true);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenUpgrades()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(true);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenMap()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(true);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenCombat()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(true);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenPauseScreen()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(true);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(true);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void OpenLose()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(true);
        WinScreen.SetActive(false);
    }

    public void OpenWin()
    {
        MainMenu.SetActive(false);
        SavesScreen.SetActive(false);
        UpgradeScreen.SetActive(false);
        MapScreen.SetActive(false);
        CombatScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(true);
    }

}
