using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEditor.Rendering;

public class PartyMember : MonoBehaviour
{
    public AudioManager audioManager;

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

    public CombatManager.DamageType attack1Type;
    public CombatManager.DamageType attack2Type;

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
    public float buttonOffset;
    public int selectedButton = 0;

    public Image attack1Cover;
    public Image attack2Cover;

    private int SelectedAttack;
    
    //Placeholder feedback
    public GameObject healDisplay;
    public GameObject dmgDisplay;

    //Character art
    public Image sprite;
    public Sprite idle;
    public Sprite windUp;
    public Sprite attack1Sprite;
    public Sprite attack2Sprite;
    public Sprite dead;
    public Color defaultColor;
    public Color hurtColor;

    //Timers
    float attackingTimer = 1;
    float currentAttackTime = 0;
    bool windingUp = false;
    float windUpTime = 0.1f;
    float currentWindUpTime = 0;
    int latestAttack;
    float hurtLength = 0.25f;
    float currentHurt = 0;

    Vector3 startPos;

    public enum Target
    {
        enemies,
        party
    }

    public Target attack1Target;
    public Target attack2Target;

    public CombatManager combatManager;

    public bool alive;

    private void Start()
    {
        startPos = transform.position;
    }

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
        //Initiate Combat
        StartCombat();
    }

    public void StartCombat()
    {
        //Resets timers and sprites. Calls for every combat
        currentCooldown = 0;
        currentAttackTime = 0;
        currentWindUpTime = 0;
        currentHurt = 0;
        windingUp = false;
        sprite.sprite = idle;
        AwakenButtons();
        if (HP <= 0) BecomeDead();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.playing)
        {

            if(combatManager.selectedButton == attack1Button)
            {
                switch(selectedButton)
                {
                    case 0:
                        selectedButton = 1;
                        attack1Button.transform.position = new Vector3(attack1Button.transform.position.x, attack1Button.transform.position.y + buttonOffset, attack1Button.transform.position.z);
                        break;
                    case 1:
                        break;
                    case 2:
                        selectedButton = 1;
                        attack1Button.transform.position = new Vector3(attack1Button.transform.position.x, attack1Button.transform.position.y + buttonOffset, attack1Button.transform.position.z);
                        attack2Button.transform.position = new Vector3(attack2Button.transform.position.x, attack2Button.transform.position.y - buttonOffset, attack2Button.transform.position.z);
                        break;
                }
            }else if (combatManager.selectedButton == attack2Button)
            {
                switch (selectedButton)
                {
                    case 0:
                        selectedButton = 2;
                        attack2Button.transform.position = new Vector3(attack2Button.transform.position.x, attack2Button.transform.position.y + buttonOffset, attack2Button.transform.position.z);
                        break;
                    case 1:
                        selectedButton = 2;
                        attack1Button.transform.position = new Vector3(attack1Button.transform.position.x, attack1Button.transform.position.y - buttonOffset, attack1Button.transform.position.z);
                        attack2Button.transform.position = new Vector3(attack2Button.transform.position.x, attack2Button.transform.position.y + buttonOffset, attack2Button.transform.position.z);
                        break;
                    case 2:
                        break;
                }
            }
            else if (combatManager.selectedButton != attack1Button && combatManager.selectedButton != attack2Button)
            {
                switch (selectedButton)
                {
                    case 0:
                        break;
                    case 1:
                        selectedButton = 0;
                        attack1Button.transform.position = new Vector3(attack1Button.transform.position.x, attack1Button.transform.position.y - buttonOffset, attack1Button.transform.position.z);
                        break;
                    case 2:
                        selectedButton = 0;
                        attack2Button.transform.position = new Vector3(attack2Button.transform.position.x, attack2Button.transform.position.y - buttonOffset, attack2Button.transform.position.z);
                        break;
                }
            }

            if (HP > 0)
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

                    attack1Cover.fillAmount = currentCooldown / cooldownLength;
                    attack2Cover.fillAmount = currentCooldown / cooldownLength;
                }
                else if (attack1Cover.IsActive() == true)
                {
                    //Allows the player to select Actions for this party member
                    AwakenButtons();
                }
            }

            if (currentHurt > 0)
            {
                sprite.color = hurtColor;
                currentHurt -= Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            }
            else
            {
                if (transform.position != startPos)
                {
                    transform.position = startPos;
                }
                if (sprite.color != defaultColor)
                {
                    sprite.color = defaultColor;
                }
                if (dmgDisplay.activeSelf)
                {
                    dmgDisplay.SetActive(false);
                }
            }
        }
    }

    //Party member is ready for action
    public void AwakenButtons()
    {
        //attack1Button.gameObject.SetActive(true);
        //attack2Button.gameObject.SetActive(true);
        attack1Button.interactable = true;
        attack2Button.interactable = true;
        attack1Cover.gameObject.SetActive(false);
        attack2Cover.gameObject.SetActive(false);
    }

    //Party member is on cooldown or dead.
    public void shutDownButtons()
    {
        attack1Button.gameObject.GetComponent<ActionButtonShowDesc>().MouseExit();
        attack2Button.gameObject.GetComponent<ActionButtonShowDesc>().MouseExit();
        //attack1Button.gameObject.SetActive(false);
        //attack2Button.gameObject.SetActive(false);
        attack1Button.interactable = true;
        attack2Button.interactable = true;
        attack1Cover.gameObject.SetActive(true);
        attack2Cover.gameObject.SetActive(true);

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
                    audioManager.PlaySFX(AudioManager.ClipToPlay.RaiseShield);
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
                    combatManager.selectedButton = attack1Button;
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
                    combatManager.selectedButton = attack2Button;
                }
                break;
        }
    }

    //On Button Press. Clicks on Target
    public void Attack(GameObject target)
    {
        //Party member did an action and is put on cooldown
        shutDownButtons();
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
                    target.GetComponent<Enemy>().TakeDMG(attack1Strength, attack1Type);
                    //Splash Damage
                    if (attack1SplashDMG > 0)
                    {
                        foreach (Enemy enemy in combatManager.enemies)
                        {
                            if (enemy != target)
                            {
                                enemy.TakeDMG(attack1SplashDMG, attack1Type);
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
                    target.GetComponent<Enemy>().TakeDMG(attack2Strength, attack2Type);
                    //Splash Damage
                    if (attack2SplashDMG > 0)
                    {
                        foreach (Enemy enemy in combatManager.enemies)
                        {
                            if (enemy != target)
                            {
                                enemy.TakeDMG(attack2SplashDMG, attack2Type);
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
        audioManager.PlaySFX(AudioManager.ClipToPlay.Heal);
        healDisplay.GetComponent<DMGDisplay>().activate();
        HP += heal;
        if (HP > MaxHP) HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    //Lose Health
    public void TakeDMG(float dmg, CombatManager.DamageType type)
    {
        HP -= dmg;
        switch (type)
        {
            case CombatManager.DamageType.Slash:
                dmgDisplay.GetComponent<Image>().sprite = combatManager.slashOnPlayer;
                break;
            case CombatManager.DamageType.Stab:
                dmgDisplay.GetComponent<Image>().sprite = combatManager.stab;
                break;
            case CombatManager.DamageType.Bash:
                dmgDisplay.GetComponent<Image>().sprite = combatManager.bash;
                break;
            case CombatManager.DamageType.Fire:
                dmgDisplay.GetComponent<Image>().sprite = combatManager.fire;
                break;
            case CombatManager.DamageType.Ice:
                dmgDisplay.GetComponent<Image>().sprite = combatManager.ice;
                break;
        }
        dmgDisplay.SetActive(true);
        if(HP <= 0)
        {
            audioManager.PlaySFX(AudioManager.ClipToPlay.Die);
            BecomeDead();
        }
        else
        {
            audioManager.PlaySFX(AudioManager.ClipToPlay.Hurt);
        }
        currentHurt = hurtLength;

        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    void BecomeDead()
    {
        sprite.sprite = dead;
        alive = false;
        HP = 0;
        if (combatManager.attacker == this) combatManager.ClearActions();
        shutDownButtons();
        attack1Cover.fillAmount = 1;
        attack2Cover.fillAmount = 1;
    }

}
