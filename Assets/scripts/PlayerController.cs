using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public PlayerMover playerMover;
    public TextMeshProUGUI textWindow;
    public SpriteRenderer playerRenderer;
    public HeartController heartController;
    

    // Attack variables(public)
    public GameObject projectile;
    public Transform visualization, shootPoint;
    public float chargeTime, maxProjectileSpeedMultiplier, minProjectileSpeedMultiplier, damage;
    

    private int textNumber = 0;
    private Inspectable CurrentInspection = null;
    private Vector2 direction = Vector2.down;

    // Attack variables(private)
    private bool fighting = false;
    private float CurrentChargetime = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    async void Update()
    {

        //Inspect
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!fighting)
            {
                if (CurrentInspection == null)
                {

                    textNumber = 0;
                    int layerMask = ~LayerMask.GetMask("player");
                    Collider2D hit = Physics2D.Raycast(transform.position, playerMover.Direction(), 1, layerMask).collider;
                    if (hit != null)
                    {

                        CurrentInspection = hit.GetComponent<Inspectable>();

                        if (CurrentInspection)
                        {
                            textWindow.text = "";
                            foreach (char c in CurrentInspection.InspectionText[textNumber])
                            {
                                textWindow.text += c;
                                await Task.Delay(50);
                            }

                            textNumber++;
                            playerMover.paralyse();
                        }
                        else CurrentInspection = null;
                    }
                }
                else if (textNumber < CurrentInspection.InspectionText.Count)
                {
                    textWindow.text = "";
                    foreach (char c in CurrentInspection.InspectionText[textNumber])
                    {
                        textWindow.text += c;
                        await Task.Delay(50);
                    }
                    textNumber++;
                }
                else
                {
                    if(CurrentInspection.InitiateFight)
                    {
                        StartFight();
                        heartController.activate();
                        CurrentInspection.enemy.GetComponent<Enemy>().StartFight();
                    }

                    CurrentInspection = null;
                    textNumber = 0;
                    textWindow.text = "";
                    playerMover.unparalyse();
                }
            }
            else
            {

            }
        }

        // Charge attack
        if(Input.GetKey(KeyCode.Space) && fighting)
        {
            if(CurrentChargetime < chargeTime)
            {
                CurrentChargetime += Time.deltaTime;
                if(CurrentChargetime > chargeTime) CurrentChargetime = chargeTime;
            }
        }


        // Attack
        if(Input.GetKeyUp(KeyCode.Space) && fighting) 
        { 
            float angle = Mathf.Atan2(-playerMover.Direction().x, playerMover.Direction().y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            projectile.transform.rotation = rotation;
            projectile.transform.position = shootPoint.position;
            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.speedMultiplier = minProjectileSpeedMultiplier + (maxProjectileSpeedMultiplier - minProjectileSpeedMultiplier)*(CurrentChargetime/chargeTime);
            bullet.damage = damage;
            
            Instantiate(bullet);

            CurrentChargetime = 0f;
        }

        if (CurrentChargetime >= 0)
            visualization.localScale = new Vector3(CurrentChargetime / chargeTime, 1, 0);
        else
            visualization.localScale = new Vector3(0, 1, 0);
        visualization.localPosition = new Vector3(-((chargeTime - CurrentChargetime) / (2 * chargeTime)), 0, visualization.localPosition.z);
    }

    public void StartFight()
    {
        playerRenderer.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        fighting = true;
    }

    public void endFight()
    {
        playerRenderer.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        fighting = false;
    }

}
