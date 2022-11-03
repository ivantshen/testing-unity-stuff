using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    private int deathTime =10;
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
        deathTime = deathTime/(int)(bulletSpeed/3.0f);
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
        if (other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<Player>()!=null){
             other.gameObject.GetComponent<Player>().decreaseHealth(bulletDamage);  
            }else if(other.gameObject.GetComponent<SentryAI>()!=null){
            other.gameObject.GetComponent<SentryAI>().decreaseHealth(bulletDamage);   
        }
        }

        if(other.gameObject.tag!="PlayerBullet"&&other.gameObject.tag!="EnemyBullet"){
          Destroy(gameObject);  
        }
        }
    }
}
