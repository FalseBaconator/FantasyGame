using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool attackSelected;
    public PartyMember.Target attackTargetType;
    public GameObject attackTarget;
    public GameObject attacker;

    public List<Enemy> enemies = new List<Enemy>();
    public List<PartyMember> partyMembers = new List<PartyMember>();

    public GameObject LoseMessage;
    public GameObject WinMessage;
    public bool playing = true;

    private void Start()
    {
        /*Debug.Log("A");
        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList<Enemy>();
        Debug.Log("B");
        partyMembers = FindObjectsByType<PartyMember>(FindObjectsSortMode.None).ToList<PartyMember>();
        Debug.Log("C");*/
    }

    private void Update()
    {
        if(attackTarget != null && attacker != null)
        {
            attacker.GetComponent<PartyMember>().Attack(attackTarget);
            attackSelected = false;
            attackTarget = null;
            attacker = null;
        }

        if(enemies.Count == 0)
        {
            Win();
        }
    }

    public void Win()
    {
        WinMessage.SetActive(true);
        foreach(PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }
        playing = false;
    }

    public void Lose()
    {
        LoseMessage.SetActive(true);
        foreach (PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }
        playing = false;
    }

}
