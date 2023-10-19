using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartyMember : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public TextMeshProUGUI HPField;

    public enum Character { Cleric, Warrior, Mage }
    public Character character;

    public UpgradeManager uManager;

    public float cooldownLength;
    public float currentCooldown;

    public float attack1Strength;
    public float attack2Strength;

    public float attack1SplashDMG;
    public float attack2SplashDMG;

    public bool attack1IsShield;
    public bool attack2IsShield;

    public bool attack1IsHeal;
    public bool attack2IsHeal;

    public Button attack1Button;
    public Button attack2Button;

    private int SelectedAttack;

    public GameObject atkDisplay;
    public GameObject dmgDisplay;
    public GameObject healDisplay;

    public enum Target
    {
        enemies,
        party
    }

    public Target attack1Target;
    public Target attack2Target;

    public CombatManager combatManager;

    public void StartCombat()
    {
        //combatManager = FindObjectOfType<CombatManager>();
        switch (character)
        {
            case Character.Cleric:
                attack1Strength = uManager.healerHeal;
                attack1IsHeal = true;
                attack1Target = Target.party;
                attack2Strength = uManager.healerAttack;
                cooldownLength = uManager.healerCooldown;
                MaxHP = uManager.healerHP;
                break;
            case Character.Warrior:
                attack1Strength = uManager.warriorShield;
                attack1IsShield = true;
                attack2Strength = uManager.warriorAttack;
                cooldownLength = uManager.warriorCooldown;
                MaxHP = uManager.warriorHP;
                break;
            case Character.Mage:
                attack1Strength = uManager.mageFire;
                attack2Strength = uManager.mageIce;
                cooldownLength = uManager.mageCooldown;
                MaxHP = uManager.mageHP;
                break;
        }
        HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
        //combatManager.partyMembers.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.playing && HP > 0)
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
            else if (attack1Button.IsActive() == false)
            {
                AwakenButtons();
            }
        }
    }

    public void AwakenButtons()
    {
        attack1Button.gameObject.SetActive(true);
        attack2Button.gameObject.SetActive(true);
    }

    public void shutDownButtons()
    {
        attack1Button.gameObject.SetActive(false);
        attack2Button.gameObject.SetActive(false);
        currentCooldown = cooldownLength;
    }

    public void SelectAttack(int attackInt)
    {
        SelectedAttack = attackInt;
        combatManager.attackSelected = true;
        combatManager.attacker = gameObject;
        switch(attackInt)
        {
            case 1:
                if (attack1IsShield)
                {
                    combatManager.ClearActions();
                    combatManager.shields += (int)attack1Strength;
                    shutDownButtons();
                }
                else
                {
                    combatManager.attackTargetType = attack1Target;
                }
                break;
            case 2:
                if (attack2IsShield)
                {
                    combatManager.ClearActions();
                    combatManager.shields += (int)attack2Strength;
                    shutDownButtons();
                }
                else
                {
                    combatManager.attackTargetType = attack2Target;
                }
                break;
        }
    }

    public void Attack(GameObject target)
    {
        shutDownButtons();
        atkDisplay.GetComponent<DMGDisplay>().activate();
        //combatManager.attackSelected = false;
        switch (SelectedAttack)
        {
            case 1:
                if(attack1Target == Target.enemies)
                {
                    target.GetComponent<Enemy>().TakeDMG(attack1Strength);
                    if (attack1SplashDMG > 0)
                    {
                        foreach (Enemy enemy in combatManager.enemies)
                        {
                            if (enemy != target)
                            {
                                enemy.TakeDMG(attack1SplashDMG);
                            }
                        }
                    }
                }else if(attack1Target == Target.party)
                {
                    if (attack1IsHeal)
                    {
                        target.GetComponent<PartyMember>().Heal(attack1Strength);
                    }
                }
                break;
            case 2:
                if (attack2Target == Target.enemies)
                {
                    target.GetComponent<Enemy>().TakeDMG(attack2Strength);
                    if (attack2SplashDMG > 0)
                    {
                        foreach (Enemy enemy in combatManager.enemies)
                        {
                            if (enemy != target)
                            {
                                enemy.TakeDMG(attack2SplashDMG);
                            }
                        }
                    }
                }
                else if (attack2Target == Target.party)
                {
                    if (attack2IsHeal)
                    {
                        target.GetComponent<PartyMember>().Heal(attack2Strength);
                    }
                }
                break;
        }
    }

    public void BecomeTarget()
    {
        if (combatManager.attackTargetType == Target.party && combatManager.attacker != null)
        {
            combatManager.attackTarget = gameObject;
        }
    }

    public void Heal(float heal)
    {
        if(HP <= 0)
        {
            combatManager.partyMembers.Add(this);
        }
        healDisplay.GetComponent<DMGDisplay>().activate();
        HP += heal;
        if (HP > MaxHP) HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    public void TakeDMG(float dmg)
    {
        dmgDisplay.GetComponent<DMGDisplay>().activate();
        HP -= dmg;
        if(HP <= 0)
        {
            HP = 0;
            if (combatManager.attacker == this) combatManager.ClearActions();
            combatManager.partyMembers.Remove(this);
            shutDownButtons();
        }

        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

}
