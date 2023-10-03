using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public TextMeshProUGUI HPField;

    public float dmg;

    public float cooldownLength;
    public float currentCooldown;

    private CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        HP = MaxHP;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
        combatManager.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.playing && HP > 0)
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= cooldownLength)
            {
                Attack();
            }
        }
    }

    /*private void OnMouseDrag()
    {
        if(combatManager.attackTargetType == PartyMember.Target.enemies)
        {
            combatManager.attacker.GetComponent<PartyMember>().Attack(gameObject);
        }
    }*/

    public void Attack()
    {
        PartyMember target;
        do {
            target = combatManager.partyMembers[Random.Range(0, combatManager.partyMembers.Count)];
        } while (target.HP <= 0);

        target.TakeDMG(dmg);

        currentCooldown = 0;
    }

    public void BecomeTarget()
    {
        if (combatManager.attackTargetType == PartyMember.Target.enemies && combatManager.attacker != null)
        {
            combatManager.attackTarget = gameObject;
        }
    }

    public void TakeDMG(float dmg)
    {
        HP -= dmg;
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
        if(HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        combatManager.enemies.Remove(this);
        GameObject.Destroy(gameObject);
    }

}
