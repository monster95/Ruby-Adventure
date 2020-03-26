using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    private PlayerController playerController;
    private Animator anim;
    float currentTime = 0f;
    float nextTime = 0f;

    private void Start()
    {
        currentTime = Time.time;
        nextTime = currentTime;
    }

    private void Update()
    {
        currentTime = currentTime + Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if(currentTime>=nextTime)
            {
                playerController.ChangeHealth(-1);
                nextTime = currentTime + 1f;
            }
            
        }
    }
}
