using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDisplay : MonoBehaviour
{


    //This is all placeholder until I can make appropriate art.

    public float cooldownLength;
    public float currentCooldown;

    public void activate()
    {
        gameObject.SetActive(true);
        currentCooldown = cooldownLength;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            gameObject.SetActive(false);

        }
    }
}
