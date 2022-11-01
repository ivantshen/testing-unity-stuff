using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public int bulletDamage;
    public float bulletSpeed;
    public GameObject bullet;
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
            GameObject newBullet = Instantiate(bullet,transform.GetChild(1).transform.position,transform.GetChild(1).transform.rotation) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            yield return new WaitForSeconds(fireRate);
            allowFire = true;
    }
}
