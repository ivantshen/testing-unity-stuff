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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)&&allowFire){
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
