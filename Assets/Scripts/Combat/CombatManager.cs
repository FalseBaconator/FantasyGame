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

    public GameObject combatCanvas;
    public GameObject LoseMessage;
    public GameObject WinMessage;
    public bool playing = true;

    public int shields;
    public TextMeshProUGUI shieldText;

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
        shieldText.text = "Shields: " + shields;

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

        if(partyMembers.Count == 0)
        {
            Lose();
        }
    }

    public void Win()
    {
        combatCanvas.SetActive(false);
        WinMessage.SetActive(true);
        foreach(PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }
        playing = false;
    }

    public void Lose()
    {
        combatCanvas.SetActive(false);
        LoseMessage.SetActive(true);
        foreach (PartyMember p in partyMembers)
        {
            p.shutDownButtons();
        }
        playing = false;
    }

}
