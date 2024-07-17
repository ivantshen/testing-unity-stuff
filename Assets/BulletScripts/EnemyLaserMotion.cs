using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    public int deathTime =8;
    private bool allowDeathTimeCD = true;
    private bool allowDamage = true;
    void FixedUpdate()
    {
        rb.velocity = transform.right*bulletSpeed;
        if(allowDeathTimeCD&&deathTime>0){
        StartCoroutine(deathTimeCountDown());
        }
    }
    void assignSpeed(float spd){
        bulletSpeed = spd;
        if(bulletSpeed!=0){
        deathTime = (int)(deathTime/(bulletSpeed/3.0f));
        }
        
    }
    void assignDeathTime(int death){
        deathTime = death;
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    IEnumerator deathTimeCountDown(){
        allowDeathTimeCD = false;
        yield return new WaitForSeconds(1);
        deathTime--;
        if(deathTime==0){
            Destroy(gameObject);
        }
        allowDeathTimeCD = true;
    }
    IEnumerator damagePlayer(Collider2D other){
        allowDamage = false;
        if(other.gameObject.GetComponent<Stats>()){
        other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage);    
        }else if(other.gameObject.GetComponent<MLStats>()){
        other.gameObject.GetComponent<MLStats>().decreaseHealth(bulletDamage);       
        }
        
        yield return new WaitForSeconds(1.25f);
        allowDamage = true;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag!="Enemy"&&other.gameObject.tag!="Boss"){
        if ((other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"||other.gameObject.tag=="PlayerBarricade")&&allowDamage){
             StartCoroutine(damagePlayer(other));
        }
        }

        if(other.gameObject.tag=="GameBarrier"){
          Destroy(gameObject);  
        }
        }
    }
