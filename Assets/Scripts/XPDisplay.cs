using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XPDisplay : MonoBehaviour
{

    public TextMeshProUGUI text;
    public int XP;
    public int speed;
    public float delay;
    public RectTransform rect;

    // Update is called once per frame
    void Update()
    {
        text.text = XP.ToString() + "XP";

        rect.position = new Vector3(rect.position.x, rect.position.y + (speed * Time.deltaTime));

        if(delay <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            delay -= Time.deltaTime;
        }

    }
}
