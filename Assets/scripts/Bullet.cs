using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, speedMultiplier;
    public float damage;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MovementDirection = transform.rotation * Vector2.up;
        rb.velocity = MovementDirection * speed * speedMultiplier;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.Damage(damage);
                Destroy(gameObject); ;
            }
        }
        if (collision.gameObject.tag.Equals("Wall")) Destroy(gameObject);
    }
}
