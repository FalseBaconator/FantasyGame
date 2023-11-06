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

    public int currentMap;
    public MapInfo[] maps;
    public int roomIndex;

    /*public int maxRoomCount;
    public GameObject[] enemyTypes;
    public GameObject boss;*/

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
    public TextMeshProUGUI title;

    public void NewAttempt()
    {
        roomIndex = 0;
        combatManager.StartPartyMembers();
        Debug.Log("A");
        GenerateNextEncounter();
    }
    
    public void GenerateNextEncounter()
    {
        title.text = maps[currentMap].title;
        Debug.Log("B");
        //combatManager.playing = true;
        roomIndex++;
        if (roomIndex < maps[currentMap].roomCount)
        {
            encounter1 = new List<GameObject>();
            enemiesInEncounter1 = rand.Next(1, 4);
            for (int i = 0; i < enemiesInEncounter1; i++)
            {
                encounter1.Add(maps[currentMap].enemyTypes[rand.Next(maps[currentMap].enemyTypes.Length)]);
            }
            encounter1Text.text = "You hear " + enemiesInEncounter1.ToString() + " enemies";

            encounter2 = new List<GameObject>();
            enemiesInEncounter2 = rand.Next(1, 4);
            for (int i = 0; i < enemiesInEncounter2; i++)
            {
                encounter2.Add(maps[currentMap].enemyTypes[rand.Next(maps[currentMap].enemyTypes.Length)]);
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
                gameManager.GoToCombat();
                combatManager.StartCombat(encounter1.ToArray());
                break;
            case 2:
                gameManager.GoToCombat();
                combatManager.StartCombat(encounter2.ToArray());
                break;
        }
    }

    public void StartBossEncounter()
    {
        gameManager.GoToCombat();
        combatManager.StartCombat(new GameObject[] { maps[currentMap].boss });
    }

    [Serializable]
    public class MapInfo
    {
        public int roomCount;
        public string title;
        public GameObject[] enemyTypes;
        public GameObject boss;
    }

}
