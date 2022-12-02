using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatstickBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    public int deathTime =8;
    private bool allowDeathTimeCD = true;
    private bool allowDamage = true;
    private bool allowMovement = true;
    public Sprite spriteToChangeTo;
    private GameObject[] gameBarriers;
    void Start(){
        gameBarriers = GameObject.FindGameObjectsWithTag("GameBarrier");
        foreach (GameObject currentBarrier in gameBarriers)
        {
            if(currentBarrier.layer==9){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),currentBarrier.GetComponent<Collider2D>());    
            }
        }
    }
    void FixedUpdate()
    {
        if(allowMovement){
        rb.velocity = transform.up*bulletSpeed;    
        }
        
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
    IEnumerator damageItem(GameObject other,int damageMult){
        allowDamage = false;
        if(other.tag=="Player"){
        other.GetComponent<PlayerMovement>().allowMovement=false;    
        }
        other.GetComponent<Stats>().speedChangePercent(0.99f,1.25f); 
        other.GetComponent<Stats>().decreaseHealth(bulletDamage*damageMult); 
        yield return new WaitForSeconds(0.75f);
        if(other&&other.tag=="Player"){
        other.GetComponent<PlayerMovement>().allowMovement=true;    
        }
        allowDamage = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag!="Boss"){
        if (other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"||other.gameObject.tag=="PlayerBarricade"){
             if(other.gameObject.tag=="Player"&&allowDamage){
                StartCoroutine(damageItem(other.gameObject,1));
             }
        }
        if(other.gameObject.tag=="Enemy"&&allowDamage){
            StartCoroutine(damageItem(other.gameObject,15));
            this.GetComponent<SpriteRenderer>().sprite = spriteToChangeTo;
            GameObject.FindWithTag("Boss").SendMessage("IncreaseRatStack");
        }
        }
        }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="GameBarrier"){
           allowMovement = false;
            rb.velocity = new Vector2(0f,0f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            rb.freezeRotation = true;;  
        }
    }
    }
