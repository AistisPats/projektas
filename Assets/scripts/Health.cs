using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    private float health;
    public Transform visualization;
    public PlayerMover playerMover;

    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 0)
            visualization.localScale = new Vector3(health / MaxHealth, 1, 0);
        else
        {
            playerMover.paralyse();
            visualization.localScale = new Vector3(0, 1, 0);
        }
        visualization.localPosition = new Vector3(-((MaxHealth - health) / (2*MaxHealth)), 0, visualization.localPosition.z);
    }

    public void heal(float health)
    {
        this.health += health;
        if(this.health > MaxHealth) health = MaxHealth; 
    }

    public bool damage(float damage)
    {
        health-=damage;
        if(health > 0) return true;
        else return false;
    }

    public bool alive()
    {
        if (health > 0) return true;
        else return false;
    }

    public float hp()
    {
        return health;
    }
}
