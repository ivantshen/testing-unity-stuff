using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public int health;
    public Rigidbody2D rb;
    public float movementSpeed;
    public int bulletDamage;
    public float bulletSpeed;
    public GameObject bullet;
    public int fireRate;
    private int specialCD = 0;
    private int frameCounter = 0;
    private int specialLength = 0;
    [SerializeField] private Camera mainCamera;
    private void Update(){
        frameCounter++;
        if(specialCD>0){
            specialCD--;
        }
        if(specialLength>0){
            specialLength--;
            if(specialLength==0){
                fireRate*=2;
                movementSpeed/=2;
            }
        }
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*movementSpeed;  
        if(Input.GetKeyDown("space")&&specialCD==0){
            fireRate/=2;
            movementSpeed*=2;
            specialCD+=2000;
            specialLength+=500;
        }
        if(Input.GetKey(KeyCode.Mouse0)&&frameCounter%fireRate==0){
            GameObject newBullet = Instantiate(bullet,transform.GetChild(1).transform.position,transform.GetChild(1).transform.rotation) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            frameCounter-=(fireRate-1);
        }
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
