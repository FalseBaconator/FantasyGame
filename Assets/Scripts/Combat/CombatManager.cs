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
    public bool playing = false;

    public int shields;
    public GameObject shieldSprite;
    public TextMeshProUGUI shieldText;

    public GameManager gameManager;

    public void StartCombat()
    {
        foreach(PartyMember partyMember in partyMembers)
        {
            partyMember.StartCombat();
        }

        System.Random rand = new System.Random();
        int enemyAmount = rand.Next(3);
        GameObject parent;
        switch (enemyAmount)
        {
            default:
            case 0:
                parent = oneEnemyParent; break;
            case 1:
                parent = twoEnemyParent; break;
            case 2:
                parent = threeEnemyParent; break;
        }
        for (int i = 0; i <= enemyAmount; i++)
        {
            Instantiate(enemyPrefabs[rand.Next(enemyPrefabs.Count)], parent.transform.GetChild(i));
        }

        playing = true;
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

        if(enemies.Count == 0 && playing)
        {
            Win();
        }

        if(partyMembers.Count == 0 && playing)
        {
            Lose();
        }
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
        gameManager.Win();
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
