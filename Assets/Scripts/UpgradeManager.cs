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

    public int warriorHP = 10;
    public float warriorCooldown = 2;
    public int warriorShield = 3;
    public int warriorAttack = 2;

    public int mageHP = 7;
    public float mageCooldown = 2;
    public int mageFire = 3;
    public int mageIce = 3;


    // Start is called before the first frame update
    void Start()
    {
        
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

    public void UpgradeFire(int amount)
    {
        mageFire = amount;
    }

    public void UpgradeIce(int amount)
    {
        mageIce = amount;
    }

}
