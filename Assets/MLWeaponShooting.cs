using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLWeaponShooting : MonoBehaviour
{
    public int bulletDamage;
    public float bulletSpeed;
    public GameObject bullet;
    public float fireRate;
    public int numBarrels;
    private bool allowFire = true;
    private float timer = 0f;
    [SerializeField] private Transform playerBullets;
    // Update is called once per frame
    void Update()
    {
        if(timer<fireRate){
            timer+=Time.deltaTime;
        }else{
            allowFire = true;
        }
    }
    public void generateBullet(){
        if(allowFire){
        timer=0f;
        allowFire = false;
        for(int i=0;i<numBarrels;i++){
            GameObject newBullet = Instantiate(bullet,transform.GetChild(i).position,transform.GetChild(i).rotation,playerBullets) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed); 
        }
        }
    }
}
