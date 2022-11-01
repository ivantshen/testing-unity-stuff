using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    // Update is called once per frame
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
        if (other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Player>().decreaseHealth(bulletDamage);
            Debug.Log(other.gameObject.GetComponent<Player>().health);
        }

        if(other.gameObject.tag!="PlayerBullet"&&other.gameObject.tag!="EnemyBullet"){
          Destroy(gameObject);  
        }
        
    }
}
