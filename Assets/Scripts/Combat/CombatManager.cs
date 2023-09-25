using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool attackSelected;
    public PartyMember.Target attackTargetType;
    public GameObject attackTarget;
    public GameObject attacker;

    public List<Enemy> enemies = new List<Enemy>();

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
        Debug.Log("WIN!");
    }

}
