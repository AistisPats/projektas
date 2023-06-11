using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAttack : MonoBehaviour
{
    public float speed, minSize;
    public float damage, stunTime;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float shrinkAmount = Time.deltaTime / speed;

        transform.localScale = new Vector3(transform.localScale.x - shrinkAmount, transform.localScale.y - shrinkAmount, transform.localScale.z);
        if(transform.localScale.x < minSize)
        {
            Destroy(gameObject);
        }

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
