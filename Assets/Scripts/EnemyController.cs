using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 2f;
    private Rigidbody2D rb;
    private Animator animator;
    int lookAt = 1;
    bool isFixed = false;
    float horizontal = 0f;
    float vertical = 0f;
    float currentTime = 0f;
    float nextTime = 0f;
    float currentHitTime = 0f;
    float nextHitTime = 0f;
    int damage = -1;
    public ParticleSystem particle;

    public AudioClip fixedClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentTime = Time.time;
        nextTime = currentTime;
    }

    // yongxiecheng
    void Update()
    {
        if(!isFixed)
        {
            currentTime += Time.deltaTime;
            currentHitTime += Time.deltaTime;
            if (currentTime > nextTime)
            {
                lookAt = lookAt % 4 + 1;
                nextTime = currentTime + 2f;
            }
            if (lookAt == 1)
            {
                horizontal = 1f;
                vertical = 0f;
            }
            else if (lookAt == 2)
            {
                horizontal = 0f;
                vertical = 1f;
            }
            else if (lookAt == 3)
            {
                horizontal = -1f;
                vertical = 0f;
            }
            else
            {
                horizontal = 0f;
                vertical = -1f;
            }

            if (horizontal != 0 || vertical != 0)
            {
                animator.SetFloat("LookX", horizontal);
                animator.SetFloat("LookY", vertical);
            }
            rb.velocity = new Vector2(horizontal, vertical) * Speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            isFixed = true;
            animator.SetTrigger("Fixed");
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(fixedClip);
            rb.velocity = new Vector2(0, 0);
            if (particle.isPlaying == true)
                particle.Stop();
        }
        else if(collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (currentHitTime > nextHitTime)
            {
                playerController.ChangeHealth(damage);
                nextHitTime = currentHitTime + 1f;
            }
        }
        
    }

}
