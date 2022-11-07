using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserShooting : MonoBehaviour
{
    public GameObject laserEndpoint;
    public int laserDamage;
    public int numLasers;
    public float fireRate;
    private bool allowFire = true;
    private LayerMask ignoreRaycastLayers;
    void Start(){
        ignoreRaycastLayers =LayerMask.GetMask("EnemyBullets")|LayerMask.GetMask("Enemies");
    }
    // Update is called once per frame
    void Update()
    {
        if(allowFire){
            StartCoroutine(fireLaser());
        }
    }
    IEnumerator fireLaser(){
        allowFire=false;
        for(int i=0;i<numLasers;i++){
        Transform firePoint = transform.GetChild(i).transform;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position,firePoint.right,ignoreRaycastLayers.value);
        LineRenderer lineRenderer = transform.GetChild(i+4).GetComponent<LineRenderer>();
        if(hit){
            if(hit.collider.gameObject.tag=="Player"||hit.collider.gameObject.tag=="Sentry"){
                hit.collider.gameObject.GetComponent<Stats>().decreaseHealth(laserDamage);
                Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
            }else if(hit.collider.gameObject.tag=="GameBarrier"){
                Instantiate(laserEndpoint,hit.point,Quaternion.identity);
                lineRenderer.SetPosition(0,firePoint.position);
                lineRenderer.SetPosition(1,hit.point);
            }
            
        }else{
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(1,firePoint.position +firePoint.right*100);
        }
        }
        yield return new WaitForSeconds(fireRate);
        allowFire =true;
    }
}
