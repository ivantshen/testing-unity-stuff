using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    void Start(){
        Physics2D.IgnoreLayerCollision(8,6,true);
        Physics2D.IgnoreLayerCollision(8,7,true);
        Physics2D.IgnoreLayerCollision(8,8,true);

    }
    void Update()
    {
        rb.velocity = transform.right*bulletSpeed;
    }
    void assignSpeed(float spd){
        bulletSpeed = spd;
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag!="Enemy"){
        if (other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<Player>()!=null){
             other.gameObject.GetComponent<Player>().decreaseHealth(bulletDamage);   
             Debug.Log(other.gameObject.GetComponent<Player>().health);
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
