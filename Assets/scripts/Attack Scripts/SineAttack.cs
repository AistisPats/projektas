using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineAttack : MonoBehaviour
{
    public float speed, frequency, amplitude;
    private float time = 0f;
    public float damage, stunTime;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float horizontalMovement = Mathf.Sin(time*frequency) * amplitude;

        Vector2 movementDirection = transform.rotation * new Vector2(horizontalMovement, 1f);

        rb.velocity = movementDirection.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Heart"))
        {
            HeartController hc = collision.GetComponent<HeartController>();
            if (hc)
            {
                hc.PlayerHealth.damage(damage);
            }
        }
        if (collision.gameObject.tag.Equals("Wall")) Destroy(gameObject);
    }
}
