using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float movementSpeed;
    public bool invincible = false;
    private float damageTakenMultiplier =1;
    public bool isPlayer = false;
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
            if(isPlayer){
                GameObject.FindWithTag("BackgroundCanvas").GetComponent<Image>().enabled = true;
                GameObject.FindWithTag("BackgroundCanvas").GetComponent<Image>().color = new Color(135,135,135,0.3f);
                Destroy(GameObject.FindWithTag("AbilityBar"));
                Destroy(GameObject.FindWithTag("HealthBar"));
                foreach(GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]){
                    if(obj.tag =="DeathScreenUI"){
                        obj.SetActive(true);
                    }
                }
            }
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
