using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{

    public Color defaultColor;
    public Color hurtColor;

    [SerializeField] Vector2 startPos;

    float hurtLength = 0.25f;
    float currentHurt = 0;

    public int shieldInt;
    public GameObject dmgDisplay;
    public Image[] sprites;
    public TextMeshProUGUI shieldText;
    public AudioManager audioManager;

    public RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect.anchoredPosition = startPos;
    }

    public void StartCombat()
    {
        rect.anchoredPosition = startPos;
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
        rect.anchoredPosition = startPos;
        if (shieldInt <= 0)
        {
            DisableSprites();
            shieldText.gameObject.SetActive(false);
        }
    }

    public void EnableSprites()
    {
        foreach (Image sprite in sprites)
        {
            sprite.enabled = true;
        }
    }

    public void DisableSprites()
    {
        foreach (Image sprite in sprites)
        {
            sprite.enabled = false;
        }
    }

    public void SwitchColour(Color colour)
    {
        foreach (Image sprite in sprites)
        {
            sprite.color = colour;
        }
    }

    public void SetShieldInt(int value)
    {
        shieldInt = value;
        shieldText.text = shieldInt.ToString();
        if (value > 0)
        {
            EnableSprites();
            shieldText.gameObject.SetActive(true);
        }
        else
        {
            DisableSprites();
            shieldText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHurt > 0)
        {
            SwitchColour(hurtColor);
            currentHurt -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - (50 * Time.deltaTime), transform.position.z);
        }
        else
        {
            if (rect.anchoredPosition != startPos)
            {
                rect.anchoredPosition = startPos;
            }
            if (sprites[0].color != defaultColor)
            {
                SwitchColour(defaultColor);
            }
            if (dmgDisplay.activeSelf)
            {
                dmgDisplay.SetActive(false);
            }
        }
    }
}
