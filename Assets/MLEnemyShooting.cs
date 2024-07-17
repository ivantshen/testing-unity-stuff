using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLEnemyShooting : MonoBehaviour
{
    public int bulletDamage;
    public int deathTime;
    public float bulletSpeed;
    public GameObject bullet;
    public int numBullets;
    public float delayBetweenBullets =0f;
    public float fireRate;
    private bool allowFire = true;
    private Transform enemyBullets;
    private float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        if(allowFire){
          allowFire = false;
          timer = 0f;
        for(int i=0;i<numBullets;i++){
          GameObject newBullet = Instantiate(bullet,transform.GetChild(i).transform.position,transform.GetChild(i).transform.rotation,transform.parent.parent) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            if(deathTime!=0){
              newBullet.SendMessage("assignDeathTime",deathTime);
            }
        }
        }else{
          timer+=Time.deltaTime;
          if(timer>fireRate){
            allowFire = true;
          }
        }
    }
    public void setEnemyBullets(Transform eb){
        enemyBullets = eb;
    }
}
