using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityHeadbutter : MonoBehaviour
{
    public int dashDamage;
    public Stats stats;
    public PlayerMovement pm;
    public int specialCD;
    public float acceleration;
    public Rigidbody2D rb;
    public float dashDuration;
    private bool isDashing = false;
    private int currentSpecialCD = 0;
    private bool allowSpecialCDDecrease = true;
    private GameObject abilityBar;
    private Vector2 currentDashPath;
    // Update is called once per frame
    void Start(){
        if(dashDuration<.35f){
            dashDuration = 0.35f;
        }
        abilityBar = GameObject.FindWithTag("MainCanvas").transform.Find("AbilityBar").gameObject;
        abilityBar.SendMessage("assignAbilityMaxCooldown",specialCD);
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    }
    void Update()
    {
        if(isDashing){
            rb.velocity-= 1.35f*Time.deltaTime*rb.velocity;
        }
        if(currentSpecialCD>0&&allowSpecialCDDecrease){
            StartCoroutine(waitForSpecialCD());
        }
        if(Input.GetKeyDown("space")&&currentSpecialCD==0){
            StartCoroutine(dash());
        }
    }
    IEnumerator dash(){
        pm.allowMovement = false;
        stats.invincible = true;
        isDashing = true;
        currentSpecialCD+=specialCD;
            currentDashPath = transform.GetChild(0).transform.right;
        rb.velocity = currentDashPath*acceleration;
        yield return new WaitForSeconds(0.35f);
        stats.invincible = false;
        yield return new WaitForSeconds(dashDuration-0.35f);
        isDashing = false;
        rb.velocity = new Vector2(0,0);
        pm.allowMovement = true;
        
        
    }
    IEnumerator waitForSpecialCD(){
    allowSpecialCDDecrease = false;
        yield return new WaitForSeconds(1);
        currentSpecialCD--;
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    allowSpecialCDDecrease = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(isDashing){
            ScreenShake.Instance.ShakeCamera(3f,0.345f);
            rb.velocity = Vector2.Reflect(currentDashPath.normalized,other.contacts[0].normal)*acceleration*0.8f;
         if(other.gameObject.tag=="Enemy"||other.gameObject.tag=="Boss"){
            other.gameObject.GetComponent<Stats>().decreaseHealth(dashDamage);
        }
        }
        }
    private void OnTriggerEnter2D(Collider2D other){
     if(isDashing&&(other.gameObject.tag=="Enemy"||other.gameObject.tag=="Boss")){
            other.gameObject.GetComponent<Stats>().decreaseHealth(dashDamage);
        }
    }
}
