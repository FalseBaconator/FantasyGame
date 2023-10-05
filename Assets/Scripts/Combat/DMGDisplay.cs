using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDisplay : MonoBehaviour
{

    public float cooldownLength;
    public float currentCooldown;

    public void activate()
    {
        gameObject.SetActive(true);
        currentCooldown = cooldownLength;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            gameObject.SetActive(false);

        }
    }
}
