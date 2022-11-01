using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityGunslinger : MonoBehaviour
{
    public int specialCD;
    public int specialLength;
    private int currentSpecialCD = 0;
    private int currentSpecialLength = 0;
    private bool allowSpecialCDDecrease = true;
    private bool allowSpecialLengthDecrease = true;
    // Update is called once per frame
    void Update()
    {
        if(currentSpecialCD>0&&allowSpecialCDDecrease){
            StartCoroutine(waitForSpecialCD());
        }
        if(currentSpecialLength>0&&allowSpecialLengthDecrease){
            StartCoroutine(waitForSpecialLength());
        }
        if(Input.GetKeyDown("space")&&currentSpecialCD==0){
            GetComponent<PlayerShooting>().fireRate/=2.0f;
            GetComponent<Player>().movementSpeed*=1.5f;
            currentSpecialLength+=specialLength;
            currentSpecialCD+=specialCD+specialLength;
        }
    }
    IEnumerator waitForSpecialCD(){
    allowSpecialCDDecrease = false;
        yield return new WaitForSeconds(1);
        currentSpecialCD--;
    allowSpecialCDDecrease = true;
    }
    IEnumerator waitForSpecialLength(){
     allowSpecialLengthDecrease = false;          
    yield return new WaitForSeconds(1);
    currentSpecialLength--;  
    if(currentSpecialLength==0){
        GetComponent<PlayerShooting>().fireRate*=2.0f;
        GetComponent<Player>().movementSpeed/=1.5f;
    }
    allowSpecialLengthDecrease = true;
    }
}
