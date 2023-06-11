 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public List<Sprite> Heart;
    public Health PlayerHealth;

    private int HeartCount;
    public PolygonCollider2D BC = null;
    public SpriteRenderer SR = null;
    


    void Start()
    {
        deactivate();
        HeartCount = Heart.Count;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < HeartCount; i++)
        {
            if(PlayerHealth.hp() > PlayerHealth.MaxHealth-((PlayerHealth.MaxHealth/(HeartCount-1))*(i+1)))
            {
                SR.sprite = Heart[i];
                break;
            }
        }
    }

    public void activate()
    {
        BC.enabled = true;
        SR.enabled = true;
    }

    public void deactivate()
    {
        BC.enabled = false;
        SR.enabled = false;
    }
}
