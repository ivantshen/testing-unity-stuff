using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float movementSpeed;
    public bool invincible = false;
    private float damageTakenMultiplier =1;
    // Start is called before the first frame update
    void Start()
    {
         health = maxHealth;
    }
    public void changeDamageTakenMultiplier(float mult){
        damageTakenMultiplier+=mult;
    }
    public void decreaseHealth(int amtToDecrease){
        if(!invincible){
         health-=(int)(amtToDecrease*damageTakenMultiplier);
        if(health<=0){
            Destroy(gameObject);
        }   
        }
        
    }
    public void increaseHealth(int amtToIncrease){
        health+=amtToIncrease;
        if(health>maxHealth){
            health = maxHealth;
        }
    }
    public void assignHealth(int hp){
        maxHealth = hp;
        health = hp;
    }
    public void speedChangePercent(float percent, float duration){
        movementSpeed *=(1+percent);
        StartCoroutine(speedReturn(percent,duration));

    }
    IEnumerator speedReturn(float percent, float duration){
        yield return new WaitForSeconds(duration);
        movementSpeed /=(1+percent);
    }
}
