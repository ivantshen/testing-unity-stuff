using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBulletMotion : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool passThroughEnemies = false;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float deathTime =9;
    void FixedUpdate()
    {
        rb.velocity = transform.right*bulletSpeed;
        if(deathTime>0){
          deathTime-=Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag!="Player"){
            if (other.gameObject.tag=="Enemy"||other.gameObject.tag=="Boss"){
            other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage);
            Destroy(gameObject);    
        }
        if(other.gameObject.tag!="PlayerBullet"&&other.gameObject.tag!="EnemyBullet"&&other.gameObject.tag!="PlayerBarricade"){
            if(!passThroughEnemies||other.gameObject.tag=="GameBarrier"){
            Destroy(gameObject);      
            }
          
        }
        }
        
    }
}