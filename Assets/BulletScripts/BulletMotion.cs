using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    private int deathTime =8;
    private bool allowDeathTimeCD = true;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,3,true);
        Physics2D.IgnoreLayerCollision(7,7,true);
        Physics2D.IgnoreLayerCollision(7,8,true);
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
        if(other.gameObject.tag!="Player"){
            if (other.gameObject.tag=="Enemy"){
            other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage);
        }
        if(other.gameObject.tag!="PlayerBullet"&&other.gameObject.tag!="EnemyBullet"){
          Destroy(gameObject);  
        }
        }
        
    }
}