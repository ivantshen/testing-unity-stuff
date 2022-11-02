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
    private bool allowHit = true;
    SpriteRenderer thisSpriteRenderer;
    void Start(){
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(7,3,true);
        Physics2D.IgnoreLayerCollision(7,7,true);
        Physics2D.IgnoreLayerCollision(7,8,true);
    }
    void FixedUpdate()
    {
        if(numberOfHits<=0){
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.right*Time.deltaTime*(bulletSpeed/10.0f));
        if(bulletSpeed>0&&allowVelocityDecrease){
            StartCoroutine(velocityFalloff());
        }
        rb.velocity = transform.right*bulletSpeed;
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
    IEnumerator damageEnemy(Collider2D other){
        allowHit=false;
        thisSpriteRenderer.color = Color.red;
        if(other){
            other.gameObject.GetComponent<EnemyAI>().movementSpeed /= 15.0f;
        }
        yield return new WaitForSeconds(0.35f);
        if(other){
         other.gameObject.GetComponent<EnemyAI>().movementSpeed *= 15.0f ;   
         other.gameObject.GetComponent<EnemyAI>().decreaseHealth(bulletDamage);
        }
        thisSpriteRenderer.color = new Color(0.6f,0.6f,0.6f);
        numberOfHits--;   
        yield return new WaitForSeconds(0.1f);
        allowHit=true;
        }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag!="Player"){
        if (other.gameObject.tag=="Enemy"&&allowHit&&numberOfHits>0){ 
            StartCoroutine(damageEnemy(other));
        }
        }
    }
}
