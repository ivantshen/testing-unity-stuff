using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAround : MonoBehaviour
{
    private Stats stats;
    private Rigidbody2D rb;
    public int damage;
    public float damageRate;
    private bool allowDamage = true;
    private bool allowDirectionChange = true;
    private bool facingRight = true;
    private float whereToGo = 17.8f;
    private bool damageAmplified = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.health>stats.maxHealth/2){
         rb.velocity = transform.right*stats.movementSpeed;
        }else{
            if(!damageAmplified){
                damageAmplified=true;
                GetComponent<SpriteRenderer>().color = new Color(0.023f,0.717f,0f,.75f);
                transform.GetChild(0).gameObject.GetComponent<TrailRenderer>().enabled = true;
                damage=(int)(damage*1.5f);
            }
        rb.velocity = transform.right*stats.movementSpeed*3;
        }
    }
    IEnumerator damagePlayer(Collision2D other,int damageToDeal,float waitTime){
        allowDamage=false;
        other.gameObject.GetComponent<Stats>().decreaseHealth(damageToDeal);
        yield return new WaitForSeconds(waitTime);
        allowDamage=true;
    }
    public IEnumerator changeDirection(){
        allowDirectionChange = false;
        if(facingRight){
             transform.rotation = Quaternion.Euler(180f,0f,180f);   
             facingRight= !facingRight;
             whereToGo*=-1;
            }else{
                transform.rotation = Quaternion.identity;
                facingRight=!facingRight;
                whereToGo*=-1;
            }
        yield return new WaitForSeconds(0.15f);
        allowDirectionChange=true;
    }
    private void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.layer==10){
          if(rb.gravityScale!=0){
            rb.gravityScale =0;
            transform.position = transform.position + new Vector3(0f,0.15f,0f);
        } 
        }else{
        if(allowDamage){
            if(other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"){
            StartCoroutine(damagePlayer(other,damage,damageRate));
            }else if(other.gameObject.tag=="PlayerBarricade"){
            StartCoroutine(damagePlayer(other,(damage*2),damageRate));
            }
            }
            if(allowDirectionChange||other.gameObject.tag=="GameBarrier"){
                StartCoroutine(changeDirection());
                if(damageAmplified){
                    ScreenShake.Instance.ShakeCamera(6f,0.515f);
                }else{
                    ScreenShake.Instance.ShakeCamera(3f,0.315f);
                }
            }    
        }
            
    }
}
