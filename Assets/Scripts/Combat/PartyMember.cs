using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEditor.Rendering;

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
    float cooldownFifth;

    public float attack1Strength;
    public float attack2Strength;

    public float attack1SplashDMG;
    public float attack2SplashDMG;

    public float attack1FreezeTime;
    public float attack2FreezeTime;

    public bool attack1IsShield;
    public bool attack2IsShield;

    public bool attack1IsHeal;
    public bool attack2IsHeal;

    public Button attack1Button;
    public Button attack2Button;

    private int SelectedAttack;
    
    //Placeholder feedback
    public GameObject atkDisplay;
    public GameObject dmgDisplay;
    public GameObject healDisplay;

    //Cooldown indicater
    public Image timer;

    //Character art
    public Image sprite;
    public Sprite idle;
    public Sprite windUp;
    public Sprite attack1Sprite;
    public Sprite attack2Sprite;
    public Sprite dead;

    //Timers
    float attackingTimer = 1;
    float currentAttackTime = 0;
    bool windingUp = false;
    float windUpTime = 0.1f;
    float currentWindUpTime = 0;
    int latestAttack;

    public enum Target
    {
        enemies,
        party
    }

    public Target attack1Target;
    public Target attack2Target;

    public CombatManager combatManager;

    public bool alive;

    public void NewAttempt()
    {
        //Get stats from Upgrade Manager Calls at the beginning of a dungeon.
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
                attack1SplashDMG = uManager.mageSplash;
                attack2Strength = uManager.mageIce;
                attack2FreezeTime = uManager.mageCool;
                cooldownLength = uManager.mageCooldown;
                MaxHP = uManager.mageHP;
                break;
        }
        HP = MaxHP;
        alive = true;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
        cooldownFifth = cooldownLength / 5;
        //Initiate Combat
        StartCombat();
    }

    public void StartCombat()
    {
        //Resets timers and sprites. Calls for every combat
        currentCooldown = 0;
        currentAttackTime = 0;
        currentWindUpTime = 0;
        sprite.sprite = idle;
        AwakenButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.playing && HP > 0)
        {
            alive = true;

            //Manage sprites for character art because I didn't want to mess with the animation window.
            if (windingUp)
            {
                if (currentWindUpTime > 0)
                {
                    currentWindUpTime -= Time.deltaTime;
                }
                else
                {
                    windingUp = false;
                    currentAttackTime = attackingTimer;
                    switch (latestAttack)
                    {
                        case 1:
                            sprite.sprite = attack1Sprite; break;
                        case 2:
                            sprite.sprite = attack2Sprite; break;
                    }
                }
            }
            else if (currentAttackTime > 0)
            {
                currentAttackTime -= Time.deltaTime;
            }
            else if (sprite.sprite != idle)
            {
                sprite.sprite = idle;
            }

            //Checks if party member is on cooldown.
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;

                //Manage cooldown indicater
                if(currentCooldown > cooldownLength - cooldownFifth)
                {
                    timer.sprite = combatManager.Timers[0];
                }else if(currentCooldown > cooldownLength - (cooldownFifth * 2))
                {
                    timer.sprite = combatManager.Timers[1];
                }
                else if (currentCooldown > cooldownLength - (cooldownFifth * 3))
                {
                    timer.sprite = combatManager.Timers[2];
                }
                else if (currentCooldown > cooldownLength - (cooldownFifth * 4))
                {
                    timer.sprite = combatManager.Timers[3];
                }
                else
                {
                    timer.sprite = combatManager.Timers[4];
                }
            }
            else if (attack1Button.IsActive() == false)
            {
                //Allows the player to select Actions for this party member
                AwakenButtons();
            }
        }
        else
        {
            //Removes the cooldown indicater when party member is dead
            timer.gameObject.SetActive(false);
        }
    }

    //Party member is ready for action
    public void AwakenButtons()
    {
        attack1Button.gameObject.SetActive(true);
        attack2Button.gameObject.SetActive(true);
        timer.gameObject.SetActive(false);
    }

    //Party member is on cooldown or dead.
    public void shutDownButtons()
    {
        attack1Button.gameObject.SetActive(false);
        attack2Button.gameObject.SetActive(false);
        timer.gameObject.SetActive(true);
        timer.sprite = combatManager.Timers[0];
        currentCooldown = cooldownLength;
    }

    //On Button Press. When player presses Action Button
    public void SelectAttack(int attackInt)
    {
        //Chooses an attack.
        SelectedAttack = attackInt;
        //Tells Combat Manager that an attack is happening
        combatManager.attackSelected = true;
        combatManager.attacker = gameObject;
        switch(attackInt)
        {
            case 1:
                //for Attack 1
                if (attack1IsShield)
                {
                    //Doesn't need a target, just activates the shield
                    combatManager.ClearActions();
                    combatManager.shields = (int)attack1Strength;
                    currentAttackTime = attackingTimer;
                    sprite.sprite = attack1Sprite;
                    shutDownButtons();
                }
                else
                {
                    //Tells Combat Manager what type of target the player should be targeting
                    combatManager.attackTargetType = attack1Target;
                }
                break;
            case 2:
                //for Attack 2
                if (attack2IsShield)
                {
                    //Doesn't need a target, just activates the shield
                    combatManager.ClearActions();
                    combatManager.shields = (int)attack2Strength;
                    currentAttackTime = attackingTimer;
                    sprite.sprite= attack2Sprite;
                    shutDownButtons();
                }
                else
                {
                    //Tells Combat Manager what type of target the player should be targeting
                    combatManager.attackTargetType = attack2Target;
                }
                break;
        }
    }

    //On Button Press. Clicks on Target
    public void Attack(GameObject target)
    {
        //Party member did an action and is put on cooldown
        shutDownButtons();
        atkDisplay.GetComponent<DMGDisplay>().activate();
        sprite.sprite = windUp;
        currentWindUpTime = windUpTime;
        windingUp = true;
        switch (SelectedAttack)
        {
            case 1:
                //For Attack 1
                latestAttack = 1;   //For sprite management
                if(attack1Target == Target.enemies)
                {
                    //Attacking opponent
                    target.GetComponent<Enemy>().TakeDMG(attack1Strength);
                    //Splash Damage
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
                    //Stuns Opponent
                    if(attack1FreezeTime > 0)
                    {
                        target.GetComponent<Enemy>().Freeze(attack1FreezeTime);
                    }
                }else if(attack1Target == Target.party)
                {
                    //Healing Ally
                    if (attack1IsHeal)
                    {
                        target.GetComponent<PartyMember>().Heal(attack1Strength);
                    }
                }
                break;
            case 2:
                //For Attack 2
                latestAttack = 2;   //For Sprite Management
                if (attack2Target == Target.enemies)
                {
                    //Attacking Opponent
                    target.GetComponent<Enemy>().TakeDMG(attack2Strength);
                    //Splash Damage
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
                    //Stuns Opponent
                    if (attack2FreezeTime > 0)
                    {
                        target.GetComponent<Enemy>().Freeze(attack2FreezeTime);
                    }
                }
                else if (attack2Target == Target.party)
                {
                    //Healing Ally
                    if (attack2IsHeal)
                    {
                        target.GetComponent<PartyMember>().Heal(attack2Strength);
                    }
                }
                break;
        }
    }

    //On Pressing the party member. Tells Combat Manager that it is the target
    public void BecomeTarget()
    {
        if (combatManager.attackTargetType == Target.party && combatManager.attacker != null)
        {
            combatManager.attackTarget = gameObject;
        }
    }

    //Gain Health
    public void Heal(float heal)
    {
        healDisplay.GetComponent<DMGDisplay>().activate();
        HP += heal;
        if (HP > MaxHP) HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    //Lose Health
    public void TakeDMG(float dmg)
    {
        dmgDisplay.GetComponent<DMGDisplay>().activate();
        HP -= dmg;
        if(HP <= 0)
        {
            sprite.sprite = dead;
            alive = false;
            HP = 0;
            if (combatManager.attacker == this) combatManager.ClearActions();
            shutDownButtons();
        }

        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

}
