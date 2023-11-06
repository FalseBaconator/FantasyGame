using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject descriptionField;
    public TextMeshProUGUI description;
    public GameObject leaveUpgradesButton;

    public int healerHP = 10;
    public float healerCooldown = 2;
    public int healerAttack = 1;
    public int healerHeal = 5;
    public int defaultHealerHP;
    public float defaultHealerCooldown;
    public int defaultHealerAttack;
    public int defaultHealerHeal;

    public int warriorHP = 10;
    public float warriorCooldown = 2;
    public int warriorShield = 3;
    public int warriorAttack = 2;
    public int defaultWarriorHP;
    public float defaultWarriorCooldown;
    public int defaultWarriorShield;
    public int defaultWarriorAttack;

    public int mageHP = 7;
    public float mageCooldown = 2;
    public int mageFire = 3;
    public float mageSplash = 0;
    public int mageIce = 3;
    public float mageCool = 0;
    public int defaultMageHP;
    public float defaultMageCooldown;
    public int defaultMageFire;
    public int defaultMageSplash;
    public int defaultMageIce;
    public float defaultMageCool;

    public List<UpgradeButton> upgradeButtons;
    public List<int> upgradeStages = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        mageIce = defaultMageIce;

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].stage = 0;
            upgradeStages[i] = 0;
        }

    }

    public void ShowDesc(string message)
    {
        description.text = message;
        descriptionField.SetActive(true);
        leaveUpgradesButton.SetActive(false);
    }

    public void HideDesc()
    {
        descriptionField.SetActive(false);
        leaveUpgradesButton.SetActive(true);
    }

    public void UpgradeHP(int character, int amount)
    {
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

    public void UpgradeCooldown(int character, float amount)
    {
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

    public void UpgradeHealerAttack(int amount)
    {
        healerAttack = amount;
    }

    public void UpgradeHeal(int amount)
    {
        healerHeal = amount;
    }

    public void UpgradeShield(int amount)
    {
        warriorShield = amount;
    }

    public void UpgradeWarriorAttack(int amount)
    {
        warriorAttack = amount;
    }

    public void UpgradeFire(int amount, int splash)
    {
        mageFire = amount;
        mageSplash = splash;
    }

    public void UpgradeIce(int amount, float cool)
    {
        mageIce = amount;
        mageCool = cool;
    }

    public void ReceiveData(int hHP, float hCooldown, int hAttack, int hHeal, int wHP, float wCooldown, int wShield, int wAttack, int mHP, float mCooldown, int mFire, int mIce, int[] stages)
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

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].stage = stages[i];
            upgradeStages[i] = stages[i];
        }

    }


}
