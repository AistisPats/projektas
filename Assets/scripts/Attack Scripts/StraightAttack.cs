using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttack : MonoBehaviour
{
    public float speed;
    public float damage, stunTime;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MovementDirection = transform.rotation * Vector2.up;
        rb.velocity = MovementDirection * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Heart"))
        {
            HeartController hc = collision.GetComponent<HeartController>();
            if(hc)
            {
                hc.PlayerHealth.damage(damage);
            }
        }
        if (collision.gameObject.tag.Equals("Wall")) Destroy(gameObject);
    }
}
