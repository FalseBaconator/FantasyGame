using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public AudioManager audioManager;

    //Data
    public float HP;
    public float MaxHP;
    public TextMeshProUGUI HPField;
    public int XP;

    public float dmg;
    public CombatManager.DamageType attackType;

    public float cooldownLength;
    public float cooldownOffset;
    public float currentCooldown;

    private CombatManager combatManager;

    //Objects to make invisible upon death.
    public GameObject[] objectsToHide;

    //Placeholder Feedback
    //public GameObject atkDisplay;
    //public GameObject dmgDisplay;
    //public GameObject healDisplay;

    public Image ice;
    public float iceTimer;

    public bool alive;

    public int dangerScore;

    public float spriteTimerCurrent;
    public float windLength;
    public float attackLength;
    public float hurtLength;

    public Image img;
    public Sprite idle;
    public Sprite windUp;
    public Sprite attack;
    public Sprite hurt;

    public GameObject dmgDisplay;

    public GameObject XPDisplay;

    // Called when generated by CombatManager
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        img.sprite = idle;
        currentCooldown = cooldownOffset;
        combatManager = FindObjectOfType<CombatManager>();
        HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
        alive = true;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BecomeTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.playing && HP > 0)
        {
            if (iceTimer > 0)   //Stunned by Mage
            {
                iceTimer -= Time.deltaTime;
            }
            else
            {
                ice.gameObject.SetActive(false);

                //Attack when Cooldown is done]
                if (img.sprite == idle)
                {
                    currentCooldown += Time.deltaTime;
                    if (currentCooldown >= cooldownLength)
                    {
                        StartAttacking();
                    }
                }
                else
                {
                    spriteTimerCurrent -= Time.deltaTime;
                    if(spriteTimerCurrent <= 0)
                    {
                        if(img.sprite == windUp)
                        {
                            Attack();
                        }
                        else
                        {
                            if (dmgDisplay.activeSelf)
                            {
                                dmgDisplay.SetActive(false);
                            }
                            img.sprite = idle;
                        }
                    }
                }
            }
        }
    }

    /*private void OnMouseDrag()            I don't know when I might need this
    {
        if(combatManager.attackTargetType == PartyMember.Target.enemies)
        {
            combatManager.attacker.GetComponent<PartyMember>().Attack(gameObject);
        }
    }*/

    //Stuns because of Ice
    public void Freeze(float time)
    {
        iceTimer = time;
        ice.gameObject.SetActive(true);
    }

    public void StartAttacking()
    {
        spriteTimerCurrent = windLength;
        img.sprite = windUp;
    }

    //Attacks a party member
    public void Attack()
    {
        spriteTimerCurrent = attackLength;
        img.sprite = attack;
        //atkDisplay.GetComponent<DMGDisplay>().activate();   //Placeholder Feedback
        if (combatManager.shield.shieldInt > 0)  //Attacks Shield
        {
            combatManager.shield.Bonk();
        }
        else    //Attacks Party Member
        {
            //Has a limit to targetting attempts in case all targets are dead
            int attempt = 0;
            PartyMember target;
            do
            {
                attempt++;
                target = combatManager.partyMembers[Random.Range(0, combatManager.partyMembers.Count)];
            } while (target.HP <= 0 && attempt < 5);

            if(target.alive) target.TakeDMG(dmg, attackType);
        }

        currentCooldown = 0;
    }

    //Get Attacked
    public void BecomeTarget()
    {
        if (combatManager.attackTargetType == PartyMember.Target.enemies && combatManager.attacker != null)
        {
            combatManager.attackTarget = gameObject;
        }
    }

    //Take Damage
    public void TakeDMG(float dmg, CombatManager.DamageType type)
    {
        spriteTimerCurrent = hurtLength;
        img.sprite = hurt;
        switch (type)
        {
            case CombatManager.DamageType.Slash:
                dmgDisplay.GetComponent<Image>().sprite = combatManager.slashOnEnemy;
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
        HP -= dmg;
        if(HP <= 0)
        {
            audioManager.PlaySFX(AudioManager.ClipToPlay.Die);
            HP = 0;
            Die();
        }
        else
        {
            audioManager.PlaySFX(AudioManager.ClipToPlay.Hurt);
        }
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    //Die
    public void Die()
    {
        alive = false;
        combatManager.GainXP(XP);
        GameObject temp = Instantiate(XPDisplay);
        temp.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        temp.GetComponent<XPDisplay>().XP = XP;
        temp.transform.parent = transform.parent.parent;
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
    }

}
