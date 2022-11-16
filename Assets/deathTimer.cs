using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTimer : MonoBehaviour
{
    public float timeTillDeath =0;
    // Update is called once per frame
    void Update()
    {
        timeTillDeath-=Time.deltaTime;
        if(timeTillDeath<=0){
            Destroy(gameObject);
        }
    }
    public void assignDeathTime(float deathTime){
        timeTillDeath = deathTime;
    }
}
