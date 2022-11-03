using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private Camera mainCamera;
    public int maxHealth;
    public int health;
    public Rigidbody2D rb;
    public float movementSpeed;
    private void Start(){
        health = maxHealth;
        mainCamera = Camera.main;
    }
    private void Update(){
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*movementSpeed;  
    }
    public void decreaseHealth(int amtToDecrease){
        health-=amtToDecrease;
        if(health<=0){
            Destroy(gameObject);
        }
    }
}
