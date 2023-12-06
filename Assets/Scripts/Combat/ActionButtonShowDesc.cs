using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonShowDesc : MonoBehaviour
{
    public GameObject desc;

    public float baseScale;
    public float hoverScale;

    private void Awake()
    {
        desc.SetActive(false);
    }

    public void MouseOver()
    {
        transform.localScale = new Vector3(hoverScale, hoverScale, hoverScale);
        desc.SetActive(true);
    }

    public void MouseExit()
    {
        transform.localScale = new Vector3(baseScale, baseScale, baseScale);
        desc.SetActive(false);
    }
}
