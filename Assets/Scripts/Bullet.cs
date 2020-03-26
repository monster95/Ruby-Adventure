using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D rb;
    float speed = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerController playerctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerctrl.getLookAt() == PlayerController.lookAt.Left)
            rb.velocity = new Vector2(-1, 0) * speed;
        else if (playerctrl.getLookAt() == PlayerController.lookAt.Right)
            rb.velocity = new Vector2(1, 0) * speed;
        else if (playerctrl.getLookAt() == PlayerController.lookAt.Up)
            rb.velocity = new Vector2(0, 1) * speed;
        else
            rb.velocity = new Vector2(0, -1) * speed;
        Destroy(this.gameObject,2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
