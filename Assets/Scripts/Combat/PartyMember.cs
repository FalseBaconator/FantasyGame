using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class PartyMember : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public TextMeshProUGUI HPField;

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

    public enum Target
    {
        enemies,
        party
    }

    public Target attack1Target;
    public Target attack2Target;

    private CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
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
        //combatManager.attackSelected = false;
        switch (SelectedAttack)
        {
            case 1:
                if(attack1Target == Target.enemies)
                {
                    target.GetComponent<Enemy>().TakeDMG(attack1Strength);
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
        HP += heal;
        if (HP > MaxHP) HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    public void TakeDMG(float dmg)
    {
        HP -= dmg;
        if(HP <= 0)
        {
            HP = 0;
            combatManager.partyMembers.Remove(this);
            shutDownButtons();
        }

        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

}
