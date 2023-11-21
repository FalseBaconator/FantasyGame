using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    public GameManager gManager;
    //public EncounterGenerator encounterGenerator;
    public Button[] levelButtons;
    public Sprite[] lockedLevels;
    public Sprite[] unlockedLevels;
    public Sprite[] completedLevels;

    public void Refresh()
    {
        int index = gManager.completedDungeons;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i <= index)
            {
                levelButtons[i].interactable = true;

                if(i < index)
                {
                    levelButtons[i].GetComponent<Image>().sprite = completedLevels[i];
                }else if (i == index)
                {
                    levelButtons[i].GetComponent<Image>().sprite = unlockedLevels[i];
                }
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = lockedLevels[i];
            }
        }
    }

    public void StartDungeon(int dungeonIndex)
    {
        switch(dungeonIndex)
        {
            case 0:
                gManager.audioManager.SwitchTrack(AudioManager.BGM.Goblin);
                break;
            case 1:
                gManager.audioManager.SwitchTrack(AudioManager.BGM.Necromancer);
                break;

        }
        gManager.StartNewDungeon(dungeonIndex);
    }


}
