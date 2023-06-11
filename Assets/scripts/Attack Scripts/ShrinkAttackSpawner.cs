using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAttackSpawner : AttackSpawner
{
    public Transform player;
    public GameObject shrinker;

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
        Instantiate(shrinker, player.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
    }
}
