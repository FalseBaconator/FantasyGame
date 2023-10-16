using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject descriptionField;
    public TextMeshProUGUI description;
    public GameObject leaveUpgradesButton;

    public int healerHP;
    public float healerCooldown;
    public int healerAttack;
    public int healerHeal;

    public int warriorHP;
    public float warriorCooldown;
    public int warriorShield;
    public int warriorAttack;

    public int mageHP;
    public float mageCooldown;
    public int mageFire;
    public int mageIce;


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
