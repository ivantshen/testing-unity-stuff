using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    public int health;
    public int contactDamage;
    public float movementSpeed;
    public float reactionTime; //Reaction time in ms
    public float contactDamageRateInSeconds; // How often the player is damaged in seconds
    public bool canCollideWithEnemy;
    public bool cantMove;
    private bool allowTracking = true;
    private bool allowDirectionChange = true;
    private bool allowContactDamage = true;
    private Camera mainCamera;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowTracking){
        StartCoroutine(waitToTrack());
        rb.velocity = transform.right*movementSpeed;  
        }  
        if(player==null&&!cantMove){
            rb.velocity = new Vector2(0,0);
        }
    }
    
    IEnumerator waitToTrack(){
        Vector3 position = transform.position;
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        float distance = Mathf.Infinity;
        if(allPlayers.Length!=0){
        foreach (GameObject currentPlayer in allPlayers){
            Vector3 diff = currentPlayer.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance<distance){
                player = currentPlayer;
                distance = currentDistance;
            }
        }
        allowTracking=false;
        Vector3 playerPosition = mainCamera.WorldToScreenPoint(player.transform.localPosition);
        Vector3 currentPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(playerPosition.x-currentPosition.x,playerPosition.y-currentPosition.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
        yield return new WaitForSeconds(reactionTime/1000.0f);
        allowTracking=true;
        }
    }
    IEnumerator directionChange(){
        allowDirectionChange = false;
        int directionRoll = Random.Range(0,2);
            if(directionRoll==0){
                rb.velocity = transform.up*movementSpeed*3;
            }else if(directionRoll==1){
                rb.velocity = transform.up*-movementSpeed*3;
            }else{
                rb.velocity = transform.right*-movementSpeed*3;
            }
        yield return new WaitForSeconds(reactionTime/5000.0f);
        allowDirectionChange = true;
    }
    IEnumerator contactDamagePlayer(Collider2D other){
        allowContactDamage = false;
        if(other.gameObject.GetComponent<Player>()!=null){
          other.gameObject.GetComponent<Player>().decreaseHealth(contactDamage);   
          Debug.Log(other.gameObject.GetComponent<Player>().health);
        }else if(other.gameObject.GetComponent<SentryAI>()!=null){
            other.gameObject.GetComponent<SentryAI>().decreaseHealth(contactDamage);   
        }
        
        yield return new WaitForSeconds(contactDamageRateInSeconds);
        allowContactDamage = true;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Enemy"&&canCollideWithEnemy&&allowDirectionChange){
            StartCoroutine(directionChange());
        }
        if(other.gameObject.tag=="Player"&&allowContactDamage){
           StartCoroutine(contactDamagePlayer(other));
        }
    }
    public void decreaseHealth(int amtToDecrease){
        health-=amtToDecrease;
        if(health<=0){
            Destroy(gameObject);
        }
    }
}
