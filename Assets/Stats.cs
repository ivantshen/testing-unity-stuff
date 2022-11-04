using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
         health = maxHealth;
    }

    public void decreaseHealth(int amtToDecrease){
        health-=amtToDecrease;
        if(health<=0){
            Destroy(gameObject);
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
}
