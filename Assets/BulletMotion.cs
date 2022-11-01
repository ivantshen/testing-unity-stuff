using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotion : MonoBehaviour
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
        if (other.gameObject.tag=="Enemy"){
            other.gameObject.GetComponent<EnemyAI>().decreaseHealth(bulletDamage);
        }
        if(other.gameObject.tag!="PlayerBullet"){
          Destroy(gameObject);  
        }
        
    }
}
