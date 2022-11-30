using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    public float deathTime = 0.45f;
    void Update()
    {
        deathTime-=Time.deltaTime;
        if(deathTime<=0){
            Destroy(gameObject);
        }
    }
    void assignSpeed(float spd){
        bulletSpeed = spd;
        if(bulletSpeed!=0){
        deathTime = (int)(deathTime/(bulletSpeed/3.0f));
        }
        
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag!="Enemy"&&other.gameObject.tag!="Boss"){
        if (other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"||other.gameObject.tag=="PlayerBarricade"){
             other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage);
        }
        }
        }
    }
