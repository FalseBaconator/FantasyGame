using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public CombatManager combatManager;
    public GameManager gameManager;
    public int roomIndex = 0;
    public int maxRoomCount = 6;
    public GameObject[] enemyTypes;
    public GameObject boss;
    public int enemiesInEncounter1;
    public List<GameObject> encounter1;
    public int enemiesInEncounter2;
    public List<GameObject> encounter2;
    System.Random rand = new System.Random();

    public GameObject[] encounterButtons;
    public GameObject bossButton;
    public TextMeshProUGUI encounter1Text;
    public TextMeshProUGUI encounter2Text;
    public TextMeshProUGUI bossEncounterText;

    public void NewAttempt()
    {
        roomIndex = 0;
        combatManager.StartPartyMembers();
        GenerateNextEncounter();
    }
    
    public void GenerateNextEncounter()
    {
        roomIndex++;
        if (roomIndex < maxRoomCount)
        {
            encounter1 = new List<GameObject>();
            enemiesInEncounter1 = rand.Next(1, 4);
            for (int i = 0; i < enemiesInEncounter1; i++)
            {
                encounter1.Add(enemyTypes[rand.Next(enemyTypes.Length)]);
            }
            encounter1Text.text = "You hear " + enemiesInEncounter1.ToString() + " enemies";

            encounter2 = new List<GameObject>();
            enemiesInEncounter2 = rand.Next(1, 4);
            for (int i = 0; i < enemiesInEncounter2; i++)
            {
                encounter2.Add(enemyTypes[rand.Next(enemyTypes.Length)]);
            }
            encounter2Text.text = "You hear " + enemiesInEncounter2.ToString() + " enemies";

            foreach (GameObject button in encounterButtons)
            {
                button.SetActive(true);
            }
            bossButton.SetActive(false);
            encounter1Text.gameObject.SetActive(true);
            encounter2Text.gameObject.SetActive(true);
            bossEncounterText.gameObject.SetActive(false);
        }
        else
        {
            foreach (GameObject button in encounterButtons)
            {
                button.SetActive(false);
            }
            bossButton.SetActive(true);
            encounter1Text.gameObject.SetActive(false);
            encounter2Text.gameObject.SetActive(false);
            bossEncounterText.gameObject.SetActive(true);
        }

    }

    public void StartEncounter(int encounterIndex)
    {
        switch (encounterIndex)
        {
            case 1:
                combatManager.StartCombat(encounter1.ToArray());
                gameManager.GoToCombat();
                break;
            case 2:
                combatManager.StartCombat(encounter2.ToArray());
                gameManager.GoToCombat();
                break;
        }
    }

    public void StartBossEncounter()
    {
        combatManager.StartCombat(new GameObject[] { boss });
        gameManager.GoToCombat();
    }

}
