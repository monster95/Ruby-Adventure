using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealth : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject collectEffect;
    private AudioSource audioSource;
    public AudioClip collect;

    private void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController!=null)
        {
            playerController.ChangeHealth(1);
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(collect);
            Destroy(this.gameObject);
        }            
    }
}
