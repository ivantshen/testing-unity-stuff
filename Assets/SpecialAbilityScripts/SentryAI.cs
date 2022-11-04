using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAI : MonoBehaviour
{
    private GameObject enemy;
    private float reactionSpeed;
    private bool allowTracking = true;
    private int timeTillDeath;
    private Camera mainCamera;
    private bool allowDeathTimeCountDown = true;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(allowDeathTimeCountDown&&timeTillDeath>0){
            StartCoroutine(deathTimeCountdown());
        }
        if(allowTracking){
        StartCoroutine(waitToTrack());
        }
    }
    IEnumerator deathTimeCountdown(){
        allowDeathTimeCountDown=false;
        timeTillDeath--;
        yield return new WaitForSeconds(1);
        if(timeTillDeath==0){
            Destroy(gameObject);
        }
        allowDeathTimeCountDown=true;
    }
     IEnumerator waitToTrack(){
        allowTracking=false;
        Vector3 position = transform.position;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        if(allEnemies.Length!=0){
        foreach (GameObject currentEnemy in allEnemies){
            Vector3 diff = currentEnemy.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance<distance){
                enemy = currentEnemy;
                distance = currentDistance;
            }
        }
        Vector3 enemyPosition = mainCamera.WorldToScreenPoint(enemy.transform.localPosition);
        Vector3 currentPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(enemyPosition.x-currentPosition.x,enemyPosition.y-currentPosition.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
        yield return new WaitForSeconds(reactionSpeed/1000.0f);
        allowTracking=true;
        }
     }
    public void assignReactionTime(float reactionTime){
        reactionSpeed = reactionTime;
    }
    public void assignDeathTime(int deathTime){
        timeTillDeath = deathTime;
    }
}
