using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    private int deathTime =8;
    private bool allowDeathTimeCD = true;
    void Start(){
        Physics2D.IgnoreLayerCollision(8,6,true);
        Physics2D.IgnoreLayerCollision(8,7,true);
        Physics2D.IgnoreLayerCollision(8,8,true);

    }
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
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag!="Enemy"){
        if (other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"){
             other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage); 
        }
        }

        if((other.gameObject.tag!="PlayerBullet")&&other.gameObject.tag!="EnemyBullet"){
          Destroy(gameObject);  
        }
        }
    }
