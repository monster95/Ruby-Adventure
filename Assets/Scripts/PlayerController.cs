using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb;
    private Animator animator;
    int CurrentHealth = 4;
    int MaxHealth = 5;
    bool isDeath = false;
    public Image HealthBar;
    public GameObject bullet;
    [HideInInspector]
    public enum lookAt { Right,Up, Down, Left  };
    private lookAt look;

    private AudioSource audioSource;
    public AudioClip attackClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        HealthBar.fillAmount = (float)CurrentHealth / MaxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Debug.Log(CurrentHealth);
        if(Input.GetKeyDown("space"))
        {
            Instantiate(bullet, transform.position + new Vector3(0f, 0.5f, 0), Quaternion.identity);
            animator.SetTrigger("Attack");
            audioSource.PlayOneShot(attackClip);
        }
             
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(horizontal!=0||vertical!=0)
        {
            animator.SetFloat("LookX", horizontal);
            animator.SetFloat("LookY", vertical);
            animator.SetFloat("Speed", Speed);
            if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical))
            {
                if (horizontal > 0)
                    look = lookAt.Right;
                else
                    look = lookAt.Left;
            }
            else
            {
                if (vertical > 0)
                    look = lookAt.Up;
                else
                    look = lookAt.Down;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        rb.velocity = new Vector2(horizontal, vertical) * Speed;
    }

    public void ChangeHealth(int health)
    {
        if(health<0)
        {
            animator.SetTrigger("Hit");
        }
        CurrentHealth += health;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            isDeath = true;
        }
        else if(CurrentHealth>MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        HealthBar.fillAmount = (float)CurrentHealth / MaxHealth;
    }

    public lookAt getLookAt()
    {
        return look;
    }
}
