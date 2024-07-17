using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDividerLaserShooting : MonoBehaviour
{
    public GameObject laserEndpoint;
    public int laserDamage;
    public int numLasers;
    public bool hasWarningLaser;
    public float warningLaserTime;
    public float fireRate;
    private bool allowFire = false;
    private bool allowWarningLaser = false;
    private bool stopWarningLaser = false;
    private LayerMask ignoreRaycastLayers;
    private float fireTimer =0f;
    void Start(){
        if(!hasWarningLaser){
            for(int i=0;i<numLasers;i++){
             transform.GetChild(i+4).GetComponent<LineRenderer>().enabled = true; 
            }
            allowFire = true;
            stopWarningLaser=true;
        }else{
            for(int i=0;i<numLasers;i++){
             transform.GetChild(i+8).GetComponent<LineRenderer>().enabled = true;  
            }
            allowWarningLaser=true;
        }
        ignoreRaycastLayers = LayerMask.GetMask("Enemies")|LayerMask.GetMask("EnemyBullets")|LayerMask.GetMask("PlayerBullets");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(warningLaserTime>=0){
            warningLaserTime-=Time.deltaTime;
        }else{
            stopWarningLaser=true;
        }
        if(fireTimer<fireRate&&stopWarningLaser){
        fireTimer+=Time.deltaTime;    
        }else if(fireTimer>=fireRate&&stopWarningLaser){
            fireTimer=0f;
            fireLaser();
        }
        if(hasWarningLaser&&allowWarningLaser){
            fireTimer+=Time.deltaTime;
            if(fireTimer>=fireRate){
                warningLaser();
            }
            if(stopWarningLaser){
            allowWarningLaser=false;
            hasWarningLaser=false;
            for(int i=0;i<numLasers;i++){
             transform.GetChild(i+8).GetComponent<LineRenderer>().enabled = false;   
            }
            for(int i=0;i<numLasers;i++){
             transform.GetChild(i+4).GetComponent<LineRenderer>().enabled = true;   
            }
            allowFire = true;
            }
            
        }
    }
    private void warningLaser(){
        allowWarningLaser=false;;
        for(int i=0;i<numLasers;i++){
        Transform firePoint = transform.GetChild(i).transform;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position,firePoint.right,100f,~ignoreRaycastLayers.value);
        LineRenderer lineRenderer = transform.GetChild(i+8).GetComponent<LineRenderer>();
        if(hit){
            if(hit.collider.gameObject.tag=="Player"||hit.collider.gameObject.tag=="Sentry"||hit.collider.gameObject.tag=="GameBarrier"){
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
        }else{
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(1,firePoint.position +firePoint.right*75);
        }
        }
        allowWarningLaser =true;
    }
    }
    private void fireLaser(){
        allowFire=false;
        for(int i=0;i<numLasers;i++){
        Transform firePoint = transform.GetChild(i).transform;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position,firePoint.right,100f,~ignoreRaycastLayers.value);
        LineRenderer lineRenderer = transform.GetChild(i+4).GetComponent<LineRenderer>();
        if(hit){
            if(hit.collider.gameObject.tag=="Player"||hit.collider.gameObject.tag=="Sentry"||hit.collider.gameObject.tag=="PlayerBarricade"){
                if(hit.collider.gameObject.GetComponent<MLStats>()){
                    hit.collider.gameObject.GetComponent<MLStats>().decreaseHealth(laserDamage);     
                }else if (hit.collider.gameObject.GetComponent<Stats>()){
                    hit.collider.gameObject.GetComponent<Stats>().decreaseHealth(laserDamage);     
                }
                Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
            }else if(hit.collider.gameObject.tag=="GameBarrier"||hit.collider.gameObject.layer==11){
                Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
            }
        }else{
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(1,firePoint.position +firePoint.right*75);
        }
        }
        allowFire =true;
    }
}