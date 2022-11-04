using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchSpin : MonoBehaviour{
    private float bulletSpeed;
    private int bulletDamage;
    // Update is called once per frame
    void Update()
    {
         transform.Rotate(Vector3.forward * (-bulletSpeed*100.0f) * Time.deltaTime);   
        
    }
    void assignSpeed(float spd){
        bulletSpeed = spd;
    }
}
