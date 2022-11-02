using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchMotionScript : MonoBehaviour
{
    
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,3,true);
        Physics2D.IgnoreLayerCollision(7,7,true);
        Physics2D.IgnoreLayerCollision(7,8,true);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right*bulletSpeed;
    }
     void assignSpeed(float spd){
        bulletSpeed = spd;
        transform.GetChild(0).SendMessage("assignSpeed",spd);
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag!="Player"){
        if (other.gameObject.tag=="Enemy"){
            other.gameObject.GetComponent<EnemyAI>().decreaseHealth(bulletDamage);
        }
        if(other.gameObject.tag!="PlayerBullet"&&other.gameObject.tag!="EnemyBullet"){
          Destroy(gameObject);  
        }
        }
    }
}
