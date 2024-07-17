using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatstickBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    public float deathTime;
    [SerializeField] private SpriteRenderer sr;
    private bool allowDamage = true;
    private bool allowMovement = true;
    private bool hitPlayerAlready=false;
    private bool hitRatAlready=false;
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
    void Update()
    {
        if(allowMovement){
        rb.velocity = transform.up*bulletSpeed;    
        }
        if(deathTime>0){
            deathTime-=Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
    }
    void assignSpeed(float spd){
        bulletSpeed = spd;
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    IEnumerator damageItem(GameObject other,int damageMult){
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
    private void OnCollisionStay2D(Collision2D other) {
        string tempTag = other.gameObject.tag;
        if(tempTag!="Boss"&&allowDamage){
            if(tempTag=="Player"&&!hitPlayerAlready){
                allowDamage = false;
                hitPlayerAlready = true;
                StartCoroutine(damageItem(other.gameObject,1));
            }else if(tempTag=="Sentry"||tempTag=="PlayerBarricade"){
                allowDamage = false;
                StartCoroutine(damageItem(other.gameObject,1));
            }
            if(tempTag=="Enemy"&&!hitRatAlready){
                hitRatAlready=true;
                allowDamage = false;
                StartCoroutine(damageItem(other.gameObject,15));
                sr.sprite = spriteToChangeTo;
                GameObject.FindWithTag("Boss").SendMessage("IncreaseRatStack");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="GameBarrier"){
           allowMovement = false;
            rb.velocity = new Vector2(0f,0f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            rb.freezeRotation = true;
            deathTime = 2.25f;
        }
    }
    }
