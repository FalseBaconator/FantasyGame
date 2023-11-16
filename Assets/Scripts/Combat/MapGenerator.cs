using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public CombatManager combatManager;
    public GameManager gameManager;

    public int currentMap;
    public MapInfo[] maps;
    public int roomIndex;

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

    public Image background;
    public Sprite[] backgrounds;

    //Start a dungeon from the first room
    public void NewAttempt()
    {
        roomIndex = 0;
        combatManager.StartPartyMembers();
        GenerateNextEncounter();
    }
    
    public void GenerateNextEncounter()
    {
        title.text = maps[currentMap].title;
        background.sprite = backgrounds[maps[currentMap].backgroundIndex];
        roomIndex++;
        //Checks if not boss room
        if (roomIndex < maps[currentMap].roomCount)
        {
            //Make encounter for first room
            encounter1 = new List<GameObject>();
            enemiesInEncounter1 = rand.Next(1, 4);
            for (int i = 0; i < enemiesInEncounter1; i++)
            {
                encounter1.Add(maps[currentMap].enemyTypes[rand.Next(maps[currentMap].enemyTypes.Length)]);
            }

            //Calculate Danger Score for first room
            int encounter1Danger = 0;
            foreach (GameObject enemy in encounter1)
            {
                encounter1Danger += enemy.GetComponent<Enemy>().dangerScore;
            }
            encounter1Text.text = "Danger Score: " + encounter1Danger.ToString();

            //Make encounter for second room
            encounter2 = new List<GameObject>();
            enemiesInEncounter2 = rand.Next(1, 4);
            for (int i = 0; i < enemiesInEncounter2; i++)
            {
                encounter2.Add(maps[currentMap].enemyTypes[rand.Next(maps[currentMap].enemyTypes.Length)]);
            }

            //Calculate Danger Score for second room
            int encounter2Danger = 0;
            foreach (GameObject enemy in encounter2)
            {
                encounter2Danger += enemy.GetComponent<Enemy>().dangerScore;
            }
            encounter2Text.text = "Danger Score: " + encounter2Danger.ToString();

            //Activate proper buttons
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
            //Activate boss room button and deactivates other buttons and text
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

    //On Button Press. Go to combat with appropriate encounter
    public void StartEncounter(int encounterIndex)
    {
        combatManager.LeaveBoss();
        switch (encounterIndex)
        {
            case 1:
                gameManager.GoToCombat();
                combatManager.StartCombat(encounter1.ToArray(), maps[currentMap].backgroundIndex);
                break;
            case 2:
                gameManager.GoToCombat();
                combatManager.StartCombat(encounter2.ToArray(), maps[currentMap].backgroundIndex);
                break;
        }
    }

    //On Button Press. Go to combat with boss encounter.
    public void StartBossEncounter()
    {
        gameManager.GoToCombat();
        combatManager.StartCombat(new GameObject[] { maps[currentMap].boss }, maps[currentMap].backgroundIndex);
    }

    //These hold the data for your different dungeon types.
    [Serializable]
    public class MapInfo
    {
        public int roomCount;
        public string title;
        public GameObject[] enemyTypes;
        public GameObject boss;
        public int backgroundIndex;
    }

}
