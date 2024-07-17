using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratBomberAI : MonoBehaviour
{
    private Vector2 positionToMoveTo;
    [SerializeField] private float moveTimeDuration;
    [SerializeField] private Transform objectToMove;
    [SerializeField] private GameObject explosionIndicator;
    [SerializeField] private GameObject cheeseBomb;
    [SerializeField] private bool randomBomb = true;
    [SerializeField] private Transform targetTransformNonRandom;
    private bool droppedBomb = false;
    private Vector2 directionVector;
    private float timer;
    private float distance;
    [SerializeField] private bool direction = true; //true = right, false = left
    private Vector2 cameraAimVector;
    private Vector2 aimVector;
    private float deathTime = 5f;
    private float cheeseBombAngle = 0f;
    private Vector2 spawnVector;
    // Start is called before the first frame update
    void Start()
    {
        if(randomBomb){
            if(Random.Range(0,1)>0.5f){
            direction = true;
            spawnVector = new Vector2(-30f,20f);
            cheeseBombAngle = 180f;
            }else{
            direction = false;
            spawnVector = new Vector2(30f,20f);
            transform.rotation = Quaternion.Euler(0f,180f,0f);
            }
        transform.position = spawnVector;
        float xAim =Random.Range(Screen.width*0.05f,Screen.width*0.95f);
        float yAim = Random.Range(Screen.height*0.1f,Screen.height*0.9f);
        cameraAimVector = new Vector2(xAim,yAim);
        aimVector = Camera.main.ScreenToWorldPoint(new Vector2(xAim,yAim));
        positionToMoveTo = new Vector2(aimVector.x,aimVector.y+7f);
        }else{
            positionToMoveTo = new Vector2(targetTransformNonRandom.position.x,targetTransformNonRandom.position.y+7f);
            cameraAimVector = Camera.main.WorldToScreenPoint(targetTransformNonRandom.position);
            aimVector = targetTransformNonRandom.position;
        }
        distance = Vector2.Distance(positionToMoveTo,objectToMove.position);
        directionVector = positionToMoveTo-(Vector2)objectToMove.position;
        if(!direction){
            transform.rotation = Quaternion.Euler(0f,180f,0f);
        }else{
            cheeseBombAngle = 180f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(!droppedBomb){
        float step = distance/moveTimeDuration * Time.deltaTime;
        objectToMove.position = Vector3.MoveTowards(objectToMove.position,positionToMoveTo,step);
        if((Vector2)objectToMove.position==positionToMoveTo){
            droppedBomb=true;
            Instantiate(explosionIndicator,cameraAimVector,Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            GameObject cheese = Instantiate(cheeseBomb,transform.position,Quaternion.Euler(0f,cheeseBombAngle,0f));
            cheese.GetComponent<moveToPosition>().changeSettings(aimVector,1.5f,0f,cheese.transform);
        }
        }else{
            float step = distance/moveTimeDuration * Time.deltaTime*1.5f;
            objectToMove.position = Vector3.MoveTowards(objectToMove.position,positionToMoveTo+directionVector*2f,step);
        }
        if(timer>=deathTime){
            Destroy(gameObject);
        }
    }
}
