using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform battlePosition;
    public float Health, AttackInterval;
    public PlayerController playerController;

    public List<AttackSpawner> attacks;

    private float TSinceLastAttack = 0f;
    private bool fighting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive()) playerController.endFight();
        if (fighting)
        {
            TSinceLastAttack += Time.deltaTime;
            if(TSinceLastAttack >= AttackInterval)
            {
                attack();
                Debug.Log(Health);
                TSinceLastAttack -= AttackInterval;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                Damage(bullet.damage);

            }
        }
    }


    public void fight()
    {
        transform.position = battlePosition.position;
    }

    private void attack()
    {
        int attackCount = attacks.Count;
        int attack = Random.Range(0, attackCount);
        attacks[attack].Spawn();
    }

    public bool Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0) return false;
        else return true;
    }

    public bool alive()
    {
        if (Health <= 0) return false;
        else return true;
    }

    public void StartFight()
    {
        fight();
        fighting = true;
    }

    public void endFight()
    {
        fighting = false;
    }
}
