using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    
    public GameManager gManager;
    public UpgradeManager uManager;
    public Button button;

    public int stage;
    public int maxStage;
    public int[] costs;
    public float[] worths;
    [TextArea] public string[] descriptions;

    public int upgradeIndex;

    public Sprite open;
    public Sprite close;
    Image img;

    private void Start()
    {
        gManager = FindFirstObjectByType<GameManager>();
        uManager = FindFirstObjectByType<UpgradeManager>();
        button = GetComponent<Button>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stage < maxStage)
        {
            if (gManager.XP < costs[stage])
            {
                button.interactable = false;
                img.sprite = close;
            }
            else
            {
                button.interactable = true;
                img.sprite = open;
            }
        }
        else
        {
            button.interactable = false;
            img.sprite = close;
        }
    }

    public void MouseOver()
    {
        if(stage < maxStage)
            uManager.ShowDesc(costs[stage] + " XP: " + descriptions[stage]);
        else
            uManager.ShowDesc(descriptions[stage]);
    }

    public void MouseExit()
    {
        uManager.HideDesc();
    }

    public void Upgrade()
    {
        gManager.XP -= costs[stage];
        switch(upgradeIndex)
        {
            case 0: //Healer HP
                uManager.UpgradeHP(0, (int)worths[stage]);
                break;
            case 1: //Healer Cooldown
                uManager.UpgradeCooldown(0, worths[stage]);
                break;
            case 2: //Healer Attack
                uManager.UpgradeHealerAttack((int)worths[stage]);
                break;
            case 3: //Healer Heal
                uManager.UpgradeHeal((int)worths[stage]);
                break;
            case 4: //Warrior HP
                uManager.UpgradeHP(1, (int)worths[stage]);
                break;
            case 5: //Warrior Cooldown
                uManager.UpgradeCooldown(1, worths[stage]);
                break;
            case 6: //Warrior Shield
                uManager.UpgradeShield((int)worths[stage]);
                break;
            case 7: //Warrior Attack
                uManager.UpgradeWarriorAttack((int)worths[stage]);
                break;
            case 8: //Mage HP
                uManager.UpgradeHP(2, (int)worths[stage]);
                break;
            case 9: //Mage Cooldown
                uManager.UpgradeCooldown(2, worths[stage]);
                break;
            case 10: //Mage Fire
                uManager.UpgradeFire((int)worths[stage]);
                break;
            case 11: //Mage Ice
                uManager.UpgradeIce((int)worths[stage]);
                break;
        }
        stage++;
        uManager.upgradeStages[uManager.upgradeButtons.IndexOf(this)]++;
        MouseOver();
    }

}
