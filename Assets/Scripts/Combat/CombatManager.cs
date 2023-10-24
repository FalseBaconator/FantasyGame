using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool attackSelected;
    public PartyMember.Target attackTargetType;
    public GameObject attackTarget;
    public GameObject attacker;

    public List<Enemy> enemies = new List<Enemy>();
    public List<PartyMember> partyMembers = new List<PartyMember>();

    public List<GameObject> enemyPrefabs;
    public GameObject oneEnemyParent;
    public GameObject twoEnemyParent;
    public GameObject threeEnemyParent;

    public GameObject combatCanvas;
    public bool playing;

    public int shields;
    public GameObject shieldSprite;
    public TextMeshProUGUI shieldText;

    public GameManager gameManager;
    public bool IsInBoss;

    public void EnterBoss()
    {
        IsInBoss = true;
    }

    public void LeaveBoss()
    {
        IsInBoss = false;
    }

    public void StartCombat(GameObject[] enemiesInEncounter)
    {
        shields = 0;
        playing = true;
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
        foreach(PartyMember partyMember in partyMembers)
        {
            partyMember.StartCombat();
        }

        System.Random rand = new System.Random();
        GameObject parent;
        //int enemyAmount = rand.Next(3);
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
            //Instantiate(enemyPrefabs[rand.Next(enemyPrefabs.Count)], parent.transform.GetChild(i));
            enemies.Add(Instantiate(enemiesInEncounter[i], parent.transform.GetChild(i)).GetComponent<Enemy>());
            enemies[i].cooldownOffset = i;
        }
    }

    public void StartPartyMembers()
    {
        foreach (PartyMember member in partyMembers)
        {
            member.NewAttempt();
        }
    }


    public void LeaveCombat()
    {
        playing = false;
    }

    private void Update()
    {
        if(shields > 0)
        {
            if(shieldSprite.activeSelf == false)
                shieldSprite.SetActive(true);
            shieldText.text = "Shields: " + shields;
        }else if(shieldSprite.activeSelf == true)
        {
            shieldSprite.SetActive(false);
        }

        if(attackTarget != null && attacker != null)
        {
            attacker.GetComponent<PartyMember>().Attack(attackTarget);
            attackSelected = false;
            attackTarget = null;
            attacker = null;
        }

        if(CheckEnemiesAlive() == false && playing)
        {
            Win();
        }

        if(CheckPlayersAlive() == false && playing)
        {
            Lose();
        }
    }

    bool CheckEnemiesAlive()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy.alive) return true;
        }
        return false;
    }

    bool CheckPlayersAlive()
    {
        foreach(PartyMember pMember in partyMembers)
        {
            if(pMember.alive) return true;
        }
        return false;
    }

    public void ClearActions()
    {
        attacker = null;
        attackSelected = false;
    }

    public void Win()
    {
        LeaveCombat();
        foreach (PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }
        if (IsInBoss)
        {
            gameManager.Win();
        }
        else
        {
            gameManager.GoToMap();
        }
    }

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
