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
    private bool allowTracking = true;
    private bool allowDirectionChange = true;
    private bool allowContactDamage = true;
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
        if(allowTracking&&player!=null){
        StartCoroutine(waitToTrack());
        rb.velocity = transform.right*movementSpeed;  
        }  
        if(player==null){
            rb.velocity = new Vector2(0,0);
        }
    }
    IEnumerator waitToTrack(){
        allowTracking=false;
        Vector3 playerPosition = mainCamera.WorldToScreenPoint(player.transform.localPosition);
        Vector3 currentPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(playerPosition.x-currentPosition.x,playerPosition.y-currentPosition.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
        yield return new WaitForSeconds(reactionTime/1000.0f);
        allowTracking=true;
    }
    IEnumerator directionChange(){
        allowDirectionChange = false;
        int directionRoll = Random.Range(0,2);
            if(directionRoll==0){
                rb.velocity = transform.up*movementSpeed*(reactionTime/1000.0f);
            }else if(directionRoll==1){
                rb.velocity = transform.up*-movementSpeed*(reactionTime/1000.0f);
            }else{
                rb.velocity = transform.right*-movementSpeed*(reactionTime/1000.0f);
            }
        yield return new WaitForSeconds(1);
        allowDirectionChange = true;
    }
    IEnumerator contactDamagePlayer(Collider2D other){
        allowContactDamage = false;
        other.gameObject.GetComponent<Player>().decreaseHealth(contactDamage); 
        Debug.Log(other.gameObject.GetComponent<Player>().health);
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
