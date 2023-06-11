using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool CanTeleport = true, justTeleported = false, paralysed = false;
    private Vector2 direction = Vector2.down;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paralysed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector2.left;
                rb.velocity = direction * speed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction = Vector2.down;
                rb.velocity = direction * speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector2.right;
                rb.velocity = direction * speed;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                direction = Vector2.up;
                rb.velocity = direction * speed;
            }
            else { rb.velocity = Vector2.zero; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "teleporter" && CanTeleport)
        {
            CanTeleport = false;
            justTeleported = true;
            Teleporter tp = collision.gameObject.GetComponent<Teleporter>();
            transform.position = new Vector3(tp.pointB.position.x, tp.pointB.position.y, transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (justTeleported) justTeleported = false;
        else if (collision.gameObject.tag == "teleporter" && !CanTeleport) CanTeleport = true;
    }

    public Vector2 Direction()
    {
        return direction;
    }

    public void paralyse()
    {
        paralysed = true;
    }

    public void unparalyse()
    {
        paralysed = false;
    }
}
