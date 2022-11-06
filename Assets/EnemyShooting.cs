using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public int bulletDamage;
    public int deathTime;
    public float bulletSpeed;
    public GameObject bullet;
    public int numBullets;
    public float fireRate;
    private bool allowFire = true;


    // Update is called once per frame
    void Update()
    {
        if(allowFire){
        StartCoroutine(generateBullet());
        }
    }
    IEnumerator generateBullet(){
        allowFire = false;
        for(int i=0;i<numBullets;i++){
          GameObject newBullet = Instantiate(bullet,transform.GetChild(i).transform.position,transform.GetChild(i).transform.rotation) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            if(deathTime!=0){
              newBullet.SendMessage("assignDeathTime",deathTime);
            }
        }
          yield return new WaitForSeconds(fireRate);    
            allowFire = true;
    }
}
