using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    public int health;
    public int contactDamage;
    public int movementSpeed;
    public int reactionTime;
    public int contactDamageRate;
    private int contactFrameCounter;
    private int frameCounter = 0;
    [SerializeField] private Camera mainCamera;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null){
        frameCounter++;
        if(frameCounter%reactionTime==0){
        Vector3 playerPosition = mainCamera.WorldToScreenPoint(player.transform.localPosition);
        Vector3 currentPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(playerPosition.x-currentPosition.x,playerPosition.y-currentPosition.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
        frameCounter-=(reactionTime-1);
        rb.velocity = transform.right*movementSpeed;  
        }  
        }else{
            rb.velocity = new Vector2(0,0);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Enemy"){
            int directionRoll = Random.Range(0,2);
            if(directionRoll==0){
                rb.velocity = transform.up*movementSpeed*(100.0f/reactionTime);
            }else if(directionRoll==1){
        rb.velocity = transform.up*-movementSpeed*(100.0f/reactionTime);
            }else{
        rb.velocity = transform.right*-movementSpeed*(100.0f/reactionTime);
            }
        }
        if(other.gameObject.tag=="Player"&&contactFrameCounter%contactDamageRate==0){
           other.gameObject.GetComponent<FollowMouse>().decreaseHealth(contactDamage); 
           Debug.Log(other.gameObject.GetComponent<FollowMouse>().health);
           contactFrameCounter-=(contactDamageRate-1);
        }
        contactFrameCounter++;
    }
    public void decreaseHealth(int amtToDecrease){
        health-=amtToDecrease;
        if(health<=0){
            Destroy(gameObject);
        }
    }
}
