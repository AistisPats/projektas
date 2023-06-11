using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttackSpawner : AttackSpawner
{
    public Transform player, minCorner, maxCorner;
    public float minDistance2Player, minSpeed, maxSpeed;
    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Spawn()
    {
        Vector2 startPoint = new Vector2(Random.Range(minCorner.position.x, maxCorner.position.x), Random.Range(minCorner.position.y, maxCorner.position.y));
        while(Vector2.Distance(startPoint, player.transform.position) < minDistance2Player)
        {
            startPoint = new Vector2(Random.Range(minCorner.position.x, maxCorner.position.x), Random.Range(minCorner.position.y, maxCorner.position.y));
            
        }
        projectile.transform.position = startPoint;

        Vector2 direction = (Vector2)player.transform.position - startPoint;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        projectile.transform.rotation = rotation;

        StraightAttack straightAttack = projectile.GetComponent<StraightAttack>();
        if(straightAttack != null )
        {
            straightAttack.speed = Random.Range(minSpeed, maxSpeed);
        }

        SineAttack sineAttack = projectile.GetComponent<SineAttack>();
        if (sineAttack != null)
        {
            sineAttack.speed = Random.Range(minSpeed, maxSpeed);
        }

        Instantiate(projectile);
    }
}
