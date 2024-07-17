using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityGunslinger : MonoBehaviour
{
    public int specialCD;
    public int specialLength;
    private int currentSpecialLength = 0;
    private bool allowSpecialLengthDecrease = true;
    private UpdateAbilityBar abilityBar;
    // Update is called once per frame
    void Awake(){
        transform.Find("GunslingerCowboyHat").GetComponent<SpriteRenderer>().enabled = false;
        abilityBar = GameObject.FindWithTag("AbilityBar").GetComponent<UpdateAbilityBar>();
        abilityBar.assignAbilityMaxCooldown(specialCD);
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")>0){
            transform.Find("GunslingerCowboyHat").transform.localRotation = Quaternion.identity;
        }else if(Input.GetAxisRaw("Horizontal")<0){
            transform.Find("GunslingerCowboyHat").transform.localRotation = Quaternion.Euler(0,180,0);
        }
        if(allowSpecialLengthDecrease&&currentSpecialLength>0){
            StartCoroutine(waitForSpecialLength());
        }
        if(abilityBar.abilityIsReady()&&Input.GetKeyDown("space")){
            abilityBar.usedAbility();
            ScreenShake.Instance.ShakeCamera(8f,0.75f);
            transform.Find("GunslingerCowboyHat").GetComponent<SpriteRenderer>().enabled = true;
              transform.GetChild(0).gameObject.GetComponent<WeaponShooting>().fireRate/=2.0f;  
               transform.GetChild(1).gameObject.GetComponent<WeaponShooting>().fireRate/=2.0f;  
            GetComponent<Stats>().movementSpeed*=1.5f;
            currentSpecialLength+=specialLength;
        }
    }
    IEnumerator waitForSpecialLength(){
     allowSpecialLengthDecrease = false;          
    yield return new WaitForSeconds(1);
    currentSpecialLength--;  
    if(currentSpecialLength==0){
        transform.Find("GunslingerCowboyHat").GetComponent<SpriteRenderer>().enabled = false;
         transform.GetChild(0).gameObject.GetComponent<WeaponShooting>().fireRate*=2.0f;  
               transform.GetChild(1).gameObject.GetComponent<WeaponShooting>().fireRate*=2.0f;  
        GetComponent<Stats>().movementSpeed/=1.5f;
    }
    allowSpecialLengthDecrease = true;
    }
}
