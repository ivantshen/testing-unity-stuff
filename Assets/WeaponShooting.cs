using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public int bulletDamage;
    public float bulletSpeed;
    public GameObject bullet;
    public float fireRate;
    public int numBarrels;
    private bool allowFire = true;
    private bool autoFiring = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            autoFiring = !autoFiring;
        }
        if((Input.GetKey(KeyCode.Mouse0)||autoFiring)&&allowFire){
            StartCoroutine(generateBullet());
        }
    }
    IEnumerator generateBullet(){
        allowFire = false;
        for(int i=0;i<numBarrels;i++){
            GameObject newBullet = Instantiate(bullet,transform.GetChild(i).position,transform.GetChild(i).rotation) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed); 
        }
            yield return new WaitForSeconds(fireRate);
            allowFire = true;
    }
}
