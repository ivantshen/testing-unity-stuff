using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public int health;
    public Rigidbody2D rb;
    public float movementSpeed;
    [SerializeField] private Camera mainCamera;
    private void Update(){
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*movementSpeed;  
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x-screenPoint.x,mouse.y-screenPoint.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
    }
    public void decreaseHealth(int amtToDecrease){
        health-=amtToDecrease;
        if(health<=0){
            Destroy(gameObject);
        }
    }
}
