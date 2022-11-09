using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlashing : MonoBehaviour
{
    public Animation anim;
    public Transform firePoint;
    public float slashRate;
    private float timeBetweenAttacks;
    public float attackRange;
    public int slashDamage;
    private bool resetPosition;
    private LayerMask targetLayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(1).gameObject.GetComponent<Animation>();
         foreach (AnimationState state in anim)
        {
            state.speed = 1.75F;
        }
        targetLayer = LayerMask.GetMask("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenAttacks<=0){
            if(anim){
              anim.Play("idle",PlayMode.StopAll);  
            }
            if(Input.GetKey(KeyCode.Mouse0)){
                ScreenShake.Instance.ShakeCamera(3f,0.215f);
                Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(firePoint.position,attackRange,targetLayer);
                foreach (Collider2D enemy in enemiesToDmg)
                {
                    enemy.GetComponent<Stats>().decreaseHealth(slashDamage);
                }
                timeBetweenAttacks = slashRate;
                if(anim){
                  anim.Play("slash", PlayMode.StopAll);  
                }
                
            }
        }else{
            timeBetweenAttacks-= Time.deltaTime;
        }
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position,attackRange);
    }
}
