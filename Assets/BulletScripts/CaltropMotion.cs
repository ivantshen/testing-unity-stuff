using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropMotion : MonoBehaviour
{
    public Rigidbody2D rb;
    private float bulletSpeed;
    private int bulletDamage;
    private bool allowVelocityDecrease = true;
    private int numberOfHits = 3;
    private int deathTime = 25;
    private bool allowDeathTimeCD = true;
    private bool allowHit = true;
    private bool rotateRight;
    private bool rotateLeft;
    SpriteRenderer thisSpriteRenderer;
    void Start(){
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if(numberOfHits<=0){
            Destroy(gameObject);
        }

        if(rotateRight){
         transform.Rotate(Vector3.back*Time.deltaTime*(bulletSpeed*22.0f));   
        }else if(rotateLeft){
        transform.Rotate(Vector3.forward*Time.deltaTime*(bulletSpeed*22.0f));   
        }   
        
        
        if(bulletSpeed>0&&allowVelocityDecrease){
            StartCoroutine(velocityFalloff());
        }
        if(allowDeathTimeCD&&deathTime>0){
    StartCoroutine(deathTimeCountDown());
        }
        rb.velocity = transform.right*bulletSpeed;
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
    IEnumerator velocityFalloff(){
        allowVelocityDecrease = false;
        bulletSpeed*=0.8f;
        yield return new WaitForSeconds(0.25f);
        allowVelocityDecrease = true;
    }
    void assignSpeed(float spd){
        bulletSpeed = spd;
    }
    void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    void assignRotateRight(bool tOrF){
        rotateRight = tOrF;
    }
    void assignRotateLeft(bool tOrF){
        rotateLeft = tOrF;
    }
    IEnumerator damageEnemy(Collider2D other){
        allowHit=false;
        thisSpriteRenderer.color = Color.red;
        if(other){
            if(other.gameObject.tag=="Boss"){
                other.gameObject.GetComponent<Stats>().speedChangePercent(-0.1f,0.55f);  
            }else{
            other.gameObject.GetComponent<Stats>().speedChangePercent(-0.5f,0.55f);    
            }
            bulletSpeed*=0.25f;
        }
        yield return new WaitForSeconds(0.55f);
        if(other){
         other.gameObject.GetComponent<Stats>().decreaseHealth(bulletDamage);
        }
        thisSpriteRenderer.color = new Color(0.6f,0.6f,0.6f);
        numberOfHits--;   
        yield return new WaitForSeconds(0.2f);
        allowHit=true;
        }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag!="Player"){
        if ((other.gameObject.tag=="Enemy"||other.gameObject.tag=="Boss")&&allowHit&&numberOfHits>0){ 
            StartCoroutine(damageEnemy(other));
        }
        }
    }
}
