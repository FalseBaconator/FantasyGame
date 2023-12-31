using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    
    public GameManager gManager;
    public UpgradeManager uManager;
    public AudioManager audioManager;
    public Button button;

    public int stage;
    public int maxStage;
    public int[] costs;
    public float[] worths;
    public float[] secondaryWorths;
    public string title;
    [TextArea] public string[] descriptions;

    public int upgradeIndex;

    public Sprite open;
    public Sprite close;
    Image img;

    public float baseScale;
    public float hoverScale;

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
        //Checks if button should be active
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

    //Show Description
    public void MouseOver()
    {
        transform.localScale = new Vector3(hoverScale, hoverScale, hoverScale);
        if(stage < maxStage)
            uManager.ShowDesc(descriptions[stage], title, costs[stage], stage - 1);
        else
            uManager.ShowDesc(descriptions[stage], title, 0, stage);
    }

    //Hide Description
    public void MouseExit()
    {
        transform.localScale = new Vector3(baseScale, baseScale, baseScale);
        uManager.HideDesc();
    }

    //On Button Press. Upgrades the stat in Upgrade Manager and on this button
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
                uManager.UpgradeFire((int)worths[stage], (int)secondaryWorths[stage]);
                break;
            case 11: //Mage Ice
                uManager.UpgradeIce((int)worths[stage], secondaryWorths[stage]);
                break;
        }
        stage++;
        uManager.upgradeStages[uManager.upgradeButtons.IndexOf(this)]++;
        MouseOver();
    }

}
