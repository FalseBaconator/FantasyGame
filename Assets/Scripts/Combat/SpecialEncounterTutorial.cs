using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEncounterTutorial : MonoBehaviour
{

    enum step { Attack, Heal, Shield, Finish};

    step currentStep;

    public GameManager gameManager;

    public Enemy enemy;

    public Shield shield;

    public PartyMember mage;

    public TextMeshProUGUI text;

    public GameObject textBox;

    public Button[] attackButtons;
    public Image[] attackCovers;
    public Button healButton;
    public Image healCover;
    public Button shieldButton;
    public Image shieldCover;

    public bool inTutorial;

    public void EnterTutorial()
    {
        inTutorial = true;
        textBox.SetActive(true);
        GoToStep(step.Attack);
    }

    public void EnterNotTutorial()
    {
        inTutorial = false;
        textBox.SetActive(false);
    }

    void GoToStep(step nextStep)
    {
        currentStep = nextStep;
        switch(nextStep)
        {
            case step.Attack:
                healButton.interactable = false;
                shieldButton.interactable = false;
                healCover.gameObject.SetActive(true);
                healCover.fillAmount = 1;
                shieldCover.gameObject.SetActive(true);
                shieldCover.fillAmount = 1;
                for (int i = 0; i < attackButtons.Length; i++)
                {
                    attackButtons[i].interactable = true;
                    attackCovers[i].gameObject.SetActive(false);
                }
                text.text = "Use one of the available buttons to attack the enemy!";
                break;
            case step.Heal:
                healButton.interactable = true;
                shieldButton.interactable = false;
                healCover.gameObject.SetActive(false);
                shieldCover.gameObject.SetActive(true);
                shieldCover.fillAmount = 1;
                enemy.img.sprite = enemy.attack;
                enemy.spriteTimerCurrent = enemy.attackLength;
                for (int i = 0; i < attackButtons.Length; i++)
                {
                    attackButtons[i].interactable = false;
                    attackCovers[i].gameObject.SetActive(true);
                    attackCovers[i].fillAmount = 1;
                }
                mage.TakeDMG(mage.MaxHP, CombatManager.DamageType.Stab);
                text.text = "Oh no! Your mage has died! Use the available button to heal them!";
                break;
            case step.Shield:
                healButton.interactable = false;
                shieldButton.interactable = true;
                healCover.gameObject.SetActive(true);
                healCover.fillAmount = 1;
                shieldCover.gameObject.SetActive(false);
                enemy.img.sprite = enemy.idle;
                for (int i = 0; i < attackButtons.Length; i++)
                {
                    attackButtons[i].interactable = false;
                    attackCovers[i].gameObject.SetActive(true);
                    attackCovers[i].fillAmount = 1;
                }
                text.text = "Use the available button to block the next few attacks";
                break;
            case step.Finish:
                healButton.interactable = true;
                shieldButton.interactable = true;
                healCover.gameObject.SetActive(false);
                shieldCover.gameObject.SetActive(false);
                for (int i = 0; i < attackButtons.Length; i++)
                {
                    attackButtons[i].interactable = true;
                    attackCovers[i].gameObject.SetActive(false);
                }
                text.text = "Now defeat your opponent!";
                inTutorial = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inTutorial)
        {
            if(enemy == null)
                enemy = FindObjectOfType<Enemy>();

            switch (currentStep)
            {
                case step.Attack:
                    if (enemy.HP < enemy.MaxHP && enemy.img.sprite == enemy.idle)
                        GoToStep(step.Heal);
                    break;
                case step.Heal:
                    if(mage.HP > 0)
                        GoToStep(step.Shield);
                    break;
                case step.Shield:
                    if (shield.shieldInt > 0)
                        GoToStep(step.Finish);
                    break;
                case step.Finish:
                    break;
            }
        }
    }
}
