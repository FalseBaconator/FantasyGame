using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject descriptionField;
    public TextMeshProUGUI upgradeTitle;
    public TextMeshProUGUI upgradeDescription;
    public Image[] stageIndicators;
    public Sprite stageEmpty;
    public Sprite stageFill;
    public GameObject toHide;

    //Healer Stats
    public int healerHP;
    public float healerCooldown;
    public int healerAttack;
    public int healerHeal;
    //Healer Default Stats (new game)
    public int defaultHealerHP;
    public float defaultHealerCooldown;
    public int defaultHealerAttack;
    public int defaultHealerHeal;

    //Warrior Stats
    public int warriorHP;
    public float warriorCooldown;
    public int warriorShield;
    public int warriorAttack;
    //Warrior Default
    public int defaultWarriorHP;
    public float defaultWarriorCooldown;
    public int defaultWarriorShield;
    public int defaultWarriorAttack;

    //Mage Stats
    public int mageHP;
    public float mageCooldown;
    public int mageFire;
    public float mageSplash;
    public int mageIce;
    public float mageCool;  //Time enemy spends stunned
    //Mage Default
    public int defaultMageHP;
    public float defaultMageCooldown;
    public int defaultMageFire;
    public int defaultMageSplash;
    public int defaultMageIce;
    public float defaultMageCool;

    public List<UpgradeButton> upgradeButtons;
    public List<int> upgradeStages = new List<int>();

    //Sets all stats to default stats
    public void returnToDefault()
    {
        healerHP = defaultHealerHP;
        healerCooldown = defaultHealerCooldown;
        healerAttack = defaultHealerAttack;
        healerHeal = defaultHealerHeal;

        warriorHP = defaultWarriorHP;
        warriorCooldown = defaultWarriorCooldown;
        warriorShield = defaultWarriorShield;
        warriorAttack = defaultWarriorAttack;

        mageHP = defaultMageHP;
        mageCooldown = defaultMageCooldown;
        mageFire = defaultMageFire;
        mageSplash = defaultMageSplash;
        mageIce = defaultMageIce;
        mageCool = defaultMageCool;

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].stage = 0;
            upgradeStages[i] = 0;
        }

    }

    //Show Description of hovered over button
    public void ShowDesc(string desc, string title, int cost, int stage)
    {
        if(cost > 0)
            upgradeTitle.text = title + ": " + cost + "XP";
        else
            upgradeTitle.text = title;
        upgradeDescription.text = desc;
        for (int i = 0; i < stageIndicators.Length; i++)
        {
            if(i <= stage)
            {
                stageIndicators[i].sprite = stageFill;
            }
            else
            {
                stageIndicators[i].sprite = stageEmpty;
            }
        }
        descriptionField.SetActive(true);
        toHide.SetActive(false);
    }

    //Hide Description field
    public void HideDesc()
    {
        descriptionField.SetActive(false);
        toHide.SetActive(true);
    }

    //Upgrade HP of designated character
    public void UpgradeHP(int character, int amount)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        switch (character)
        {
            case 0:
                healerHP = amount;
                break;
            case 1:
                warriorHP = amount;
                break;
            case 2:
                mageHP = amount;
                break;
        }
    }

    //Upgrade Cooldown of designated character
    public void UpgradeCooldown(int character, float amount)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        switch (character)
        {
            case 0:
                healerCooldown = amount;
                break;
            case 1:
                warriorCooldown = amount;
                break;
            case 2:
                mageCooldown = amount;
                break;
        }
    }

    //Upgrade Actions
    public void UpgradeHealerAttack(int amount)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        healerAttack = amount;
    }

    public void UpgradeHeal(int amount)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        healerHeal = amount;
    }

    public void UpgradeShield(int amount)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        warriorShield = amount;
    }

    public void UpgradeWarriorAttack(int amount)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        warriorAttack = amount;
    }

    public void UpgradeFire(int amount, int splash)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        mageFire = amount;
        mageSplash = splash;
    }

    public void UpgradeIce(int amount, float cool)
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Upgrade);
        mageIce = amount;
        mageCool = cool;
    }

    //Gets data from another source, for loading save files.
    public void ReceiveData(int hHP, float hCooldown, int hAttack, int hHeal, int wHP, float wCooldown, int wShield, int wAttack, int mHP, float mCooldown, int mFire, int mIce, float mSplash, float mCool, int[] stages)
    {
        healerHP = hHP;
        healerCooldown = hCooldown;
        healerAttack = hAttack;
        healerHeal = hHeal;

        warriorHP = wHP;
        warriorCooldown = wCooldown;
        warriorShield = wShield;
        warriorAttack = wAttack;

        mageHP = mHP;
        mageCooldown = mCooldown;
        mageFire = mFire;
        mageIce = mIce;
        mageSplash = mSplash;
        mageCool = mCool;

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].stage = stages[i];
            upgradeStages[i] = stages[i];
        }

    }


}
