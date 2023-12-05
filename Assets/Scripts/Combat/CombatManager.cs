using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    //Damage Type Data
    public enum DamageType { Slash, Stab, Bash, Fire, Ice, None};
    public Sprite slashOnPlayer;
    public Sprite slashOnEnemy;
    public Sprite stab;
    public Sprite bash;
    public Sprite fire;
    public Sprite ice;

    //attacking data
    public bool attackSelected;
    public PartyMember.Target attackTargetType;
    public GameObject attackTarget;
    public GameObject attacker;

    //possible targets
    public List<Enemy> enemies = new List<Enemy>();
    public List<PartyMember> partyMembers = new List<PartyMember>();

    //Enemy generation
    public List<GameObject> enemyPrefabs;
    public GameObject oneEnemyParent;
    public GameObject twoEnemyParent;
    public GameObject threeEnemyParent;

    //Is the combat running data
    public GameObject combatCanvas;
    public bool playing;

    public Shield shield;

    public GameManager gameManager;
    public bool IsInBoss;

    //Cooldown Indicater Data
    public Sprite[] Timers;

    //Background
    public Image background;
    public Sprite[] backgrounds;

    public Button selectedButton;

    public float endDelay;
    public float bossEndDelay;
    private float currentEndDelay;
    public bool winning;
    public bool losing;

    //Is In Boss Room?
    public void EnterBoss()
    {
        IsInBoss = true;
    }

    public void LeaveBoss()
    {
        IsInBoss = false;
    }

    //Starts combat with encouter given to it by EncounterGenerator
    public void StartCombat(GameObject[] enemiesInEncounter, int backgroundIndex)
    {
        winning = false;
        losing = false;
        ClearActions();
        background.sprite = backgrounds[backgroundIndex];
        shield.SetShieldInt(0);
        //Remove previous encounter's enemies
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
        //preps Party Members for a new round of combat
        foreach(PartyMember partyMember in partyMembers)
        {
            partyMember.StartCombat();
        }

        //Places Enemies
        GameObject parent;
        switch (enemiesInEncounter.Length)
        {
            default:
            case 1:
                parent = oneEnemyParent; break;
            case 2:
                parent = twoEnemyParent; break;
            case 3:
                parent = threeEnemyParent; break;
        }
        for (int i = 0; i < enemiesInEncounter.Length; i++)
        {
            enemies.Add(Instantiate(enemiesInEncounter[i], parent.transform.GetChild(i)).GetComponent<Enemy>());
            enemies[i].cooldownOffset = i;
        }
    }

    public void ShowTargets(PartyMember.Target target)
    {
        switch (target)
        {
            case PartyMember.Target.enemies:
                foreach(Enemy enemy in enemies)
                {
                    if (enemy.alive) enemy.ShowBorder();
                }
                foreach (PartyMember partyMember in partyMembers)
                {
                    partyMember.HideBorder();
                }
                break;
            case PartyMember.Target.party:
                foreach(PartyMember partyMember in partyMembers)
                {
                    partyMember.ShowBorder();
                }
                foreach (Enemy enemy in enemies)
                {
                    enemy.HideBorder();
                }
                break;
        }
    }

    //Preps party members for a new dungeon. Upon entering Map through Upgrades
    public void StartPartyMembers()
    {
        foreach (PartyMember member in partyMembers)
        {
            member.NewAttempt();
        }
        shield.StartCombat();
    }

    //No Longer In combat
    public void LeaveCombat()
    {
        winning = false;
        losing = false;
        playing = false;
    }

    private void Update()
    {
        //Initiates an attack by Party Member when both Action and Target are selected. Bypassed by Shield
        if(attackTarget != null && attacker != null)
        {
            attacker.GetComponent<PartyMember>().Attack(attackTarget);
            ClearActions();
        }

        //If combat success
        if(CheckEnemiesAlive() == false && playing && !winning)
        {
            winning = true;
            if(!IsInBoss)
                currentEndDelay = endDelay;
            else
                currentEndDelay = bossEndDelay;
        }

        //If combat loss
        if(CheckPlayersAlive() == false && playing && !losing)
        {
            losing = true;
            currentEndDelay = endDelay;
        }

        if(winning || losing)
        {
            currentEndDelay -= Time.deltaTime;
            if(currentEndDelay <= 0)
            {
                if (winning) Win();
                else if (losing) Lose();
            }
        }

    }

    //Checks to see if there are living enemies
    bool CheckEnemiesAlive()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy.alive) return true;
        }
        return false;
    }

    //Checks to see if there are living party members
    bool CheckPlayersAlive()
    {
        foreach(PartyMember pMember in partyMembers)
        {
            if(pMember.alive) return true;
        }
        return false;
    }

    //Removes Action data
    public void ClearActions()
    {
        attacker = null;
        attackSelected = false;
        attackTarget = null;
        selectedButton = null;
        foreach (PartyMember partyMember in partyMembers)
        {
            partyMember.HideBorder();
        }
        foreach (Enemy enemy in enemies)
        {
            enemy.HideBorder();
        }
    }

    //Upon win
    public void Win()
    {
        LeaveCombat();
        foreach (PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }

        //Go to Win or Map
        if (IsInBoss)
        {
            gameManager.Win();
        }
        else
        {
            gameManager.GoToEncounterGen();
        }
    }

    //Party Defeated
    public void Lose()
    {
        LeaveCombat();
        foreach (PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }
        gameManager.Lose();
    }

}
