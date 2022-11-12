using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchMotionScript : MonoBehaviour
{
    
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    private int deathTime =4;
    private bool allowDeathTimeCD = true;
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
        if(allowDeathTimeCD&&deathTime>0){
StartCoroutine(deathTimeCountDown());
        }
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
     void assignSpeed(float spd){
        bulletSpeed = spd;
        transform.GetChild(0).SendMessage("assignSpeed",spd);
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag!="Player"){
        if (other.gameObject.tag=="Enemy"||other.gameObject.tag=="Boss"){
            other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage);
        }
        if(other.gameObject.tag!="PlayerBullet"&&other.gameObject.tag!="EnemyBullet"&&other.gameObject.tag!="PlayerBarricade"){
          Destroy(gameObject);  
        }
        }
    }
}
