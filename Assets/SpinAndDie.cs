using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndDie : MonoBehaviour
{
    public float spinSpeed;
    public float duration;
    private bool allowCountDown = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * (spinSpeed*10.0f) * Time.deltaTime);   
        if(allowCountDown){
          StartCoroutine(Countdown());  
        }
        
    }
    IEnumerator Countdown(){
    allowCountDown =false;
    duration--;
    yield return new WaitForSeconds(1);
    if(duration==0){
        Destroy(gameObject);
    }
    allowCountDown = true;
    }
}
