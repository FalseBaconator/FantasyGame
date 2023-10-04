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

    public GameObject[] objectsToHide;

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
        if (combatManager.shields > 0)
        {
            combatManager.shields--;
        }
        else
        {
            PartyMember target;
            do
            {
                target = combatManager.partyMembers[Random.Range(0, combatManager.partyMembers.Count)];
            } while (target.HP <= 0);

            target.TakeDMG(dmg);
        }

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
        if(HP <= 0)
        {
            HP = 0;
            Die();
        }
        HPField.text = HP.ToString() + "/" + MaxHP.ToString();
    }

    public void Die()
    {
        combatManager.enemies.Remove(this);
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
        GameObject.Destroy(gameObject);
    }

}
