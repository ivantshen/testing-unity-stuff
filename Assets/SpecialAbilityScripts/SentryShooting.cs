using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryShooting : MonoBehaviour
{
    private int bulletDamage;
    private float bulletSpeed;
    public GameObject bullet;
    private float fireRate;
    private bool allowFire = true;

    public void assignBulletSpeed(float bulletspd){
        bulletSpeed = bulletspd;
    }
    public void assignDamage(int dmg){
        bulletDamage = dmg;
    }
    public void assignFireRate(float frate){
        fireRate = frate;
    }
    // Update is called once per frame
     void Update()
    {
        if(allowFire){
        StartCoroutine(generateBullet());
        }
    }
    IEnumerator generateBullet(){
        allowFire = false;
            GameObject newBullet = Instantiate(bullet,transform.GetChild(1).transform.position,transform.GetChild(1).transform.rotation) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            yield return new WaitForSeconds(fireRate);
            allowFire = true;
    }
}
