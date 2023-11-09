using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonShowDesc : MonoBehaviour
{
    public GameObject desc;

    private void Awake()
    {
        desc.SetActive(false);
    }

    public void MouseOver()
    {
        desc.SetActive(true);
    }

    public void MouseExit()
    {
        desc.SetActive(false);
    }
}
