using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    public GameManager gManager;
    //public EncounterGenerator encounterGenerator;
    public Button[] levelButtons;

    public void Refresh()
    {
        int index = gManager.completedDungeons;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i <= index)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void StartDungeon(int dungeonIndex)
    {
        gManager.StartNewDungeon(dungeonIndex);
    }


}
