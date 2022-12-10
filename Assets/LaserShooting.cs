using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : MonoBehaviour
{
    public GameObject laserEndpoint;
    public GameObject warningSign;
    public float warningLaserTime;
    public float fireRate;
    private bool hasWarningLaser = true;
    private bool allowFire = false;
    private bool allowWarningLaser = true;
    private bool stopWarningLaser = false;
    private GameObject[] firstFiveObjectsHit = new GameObject[5];
    private int currentGameObjectIndex = 0;
    private bool stopLaser = true;
     private LayerMask ignoreRaycastLayers;
     private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        ignoreRaycastLayers = LayerMask.GetMask("EnemyBullets")|LayerMask.GetMask("PlayerBullets");
        transform.GetChild(1).GetComponent<LineRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
         if(warningLaserTime>=0){
            warningLaserTime-=Time.deltaTime;
        }else{
            stopWarningLaser=true;
        }
        if(allowFire&&!stopLaser){
            StartCoroutine(fireLaser());
        }
        if(hasWarningLaser&&allowWarningLaser){
            if(stopWarningLaser){
            allowWarningLaser=false;
            hasWarningLaser=false;
            stopLaser=false;
            transform.GetChild(1).GetComponent<LineRenderer>().enabled = false;
            transform.GetChild(0).GetComponent<LineRenderer>().enabled = true;   
            allowFire = true;
            }
            StartCoroutine(warningLaser());
        }
    }
    IEnumerator warningLaser(){
        allowWarningLaser=false;
        Transform firePoint = transform;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position,firePoint.right,100f,~ignoreRaycastLayers.value);
        LineRenderer lineRenderer = transform.GetChild(1).GetComponent<LineRenderer>();
        if(hit){
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
        }
        yield return new WaitForSeconds(fireRate);
        allowWarningLaser =true;
    }
    IEnumerator fireLaser(){
        allowFire=false;
        Transform firePoint = transform;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position,firePoint.right,100f,~ignoreRaycastLayers.value);
        LineRenderer lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
        if(hit){
            if(currentGameObjectIndex<5){
            if(hit.collider.gameObject.tag=="Player"||hit.collider.gameObject.tag=="Sentry"||hit.collider.gameObject.tag=="Enemy"){
                if(hit.collider.gameObject.GetComponent<Stats>().movementSpeed>0.025f){
                hit.collider.gameObject.GetComponent<Stats>().speedChangePercent(-0.99f,1.25f);    
                }
                
                        if(currentGameObjectIndex==0){
                            firstFiveObjectsHit[currentGameObjectIndex] = hit.collider.gameObject;
                            boss.GetComponent<NathanYuAI>().StartCoroutine("addToThrowQueue",hit.collider.gameObject);
                            GameObject warning = Instantiate(warningSign,Camera.main.WorldToScreenPoint(hit.point),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
                            warning.SendMessage("assignDeathTime",3.0f);
                            currentGameObjectIndex++;
                        }
                        if(currentGameObjectIndex<5){
                            bool inArray = false;
                            for(int i=0;i<currentGameObjectIndex;i++){
                                if(hit.collider.gameObject==firstFiveObjectsHit[i]){
                                inArray = true;
                                }
                            }   
                            if(!inArray){
                                firstFiveObjectsHit[currentGameObjectIndex] = hit.collider.gameObject;
                                boss.SendMessage("addToThrowQueue",hit.collider.gameObject);
                                GameObject warning = Instantiate(warningSign,Camera.main.WorldToScreenPoint(hit.point),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
                                 warning.SendMessage("assignDeathTime",3.0f);
                                currentGameObjectIndex++;
                            }
                        }
                  Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);      
                }else if(hit.collider.gameObject.tag=="GameBarrier"||hit.collider.gameObject.layer==11){
                Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
             }else if(hit.collider.gameObject.tag=="PlayerBarricade"){
                hit.collider.gameObject.GetComponent<Stats>().decreaseHealth(5);
                Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
            }
                
            }
            }else{
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(1,firePoint.position +firePoint.right*75);
            }
          yield return new WaitForSeconds(fireRate);
          allowFire =true;   
        }
       
        }
