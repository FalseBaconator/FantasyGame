using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{

    public Color defaultColor;
    public Color hurtColor;

    Vector3 startPos;

    float hurtLength = 0.25f;
    float currentHurt = 0;

    public int shieldInt;
    public GameObject dmgDisplay;
    public Image sprite;
    public TextMeshProUGUI shieldText;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    public void StartCombat()
    {
        transform.position = startPos;
        currentHurt = 0;
        GetComponent<Image>().color = defaultColor;
    }

    public void Bonk()
    {
        audioManager.PlaySFX(AudioManager.ClipToPlay.Block);
        shieldInt--;
        shieldText.text = shieldInt.ToString();
        currentHurt = hurtLength;
        dmgDisplay.SetActive(true);
        transform.position = startPos;
        if (shieldInt <= 0)
        {
            sprite.enabled = false;
            shieldText.gameObject.SetActive(false);
        }
    }

    public void SetShieldInt(int value)
    {
        shieldInt = value;
        shieldText.text = shieldInt.ToString();
        if (value > 0)
        {
            sprite.enabled = true;
            shieldText.gameObject.SetActive(true);
        }
        else
        {
            sprite.enabled = false;
            shieldText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHurt > 0)
        {
            sprite.color = hurtColor;
            currentHurt -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - (50 * Time.deltaTime), transform.position.z);
        }
        else
        {
            if (transform.position != startPos)
            {
                transform.position = startPos;
            }
            if (sprite.color != defaultColor)
            {
                sprite.color = defaultColor;
            }
            if (dmgDisplay.activeSelf)
            {
                dmgDisplay.SetActive(false);
            }
        }
    }
}
