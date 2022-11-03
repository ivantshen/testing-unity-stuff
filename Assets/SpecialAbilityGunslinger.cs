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
    private GameObject abilityBar;
    // Update is called once per frame
    void Start(){
        transform.Find("GunslingerCowboyHat").GetComponent<SpriteRenderer>().enabled = false;
        abilityBar = GameObject.FindWithTag("MainCanvas").transform.Find("AbilityBar").gameObject;
        abilityBar.SendMessage("assignAbilityMaxCooldown",specialCD);
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")>0){
            transform.Find("GunslingerCowboyHat").transform.localRotation = Quaternion.identity;
        }else if(Input.GetAxisRaw("Horizontal")<0){
            transform.Find("GunslingerCowboyHat").transform.localRotation = Quaternion.Euler(0,180,0);
        }
        if(allowSpecialCDDecrease&&currentSpecialCD>0){
            StartCoroutine(waitForSpecialCD());
        }
        if(allowSpecialLengthDecrease&&currentSpecialLength>0){
            StartCoroutine(waitForSpecialLength());
        }
        if(currentSpecialCD==0&&Input.GetKeyDown("space")){
            transform.Find("GunslingerCowboyHat").GetComponent<SpriteRenderer>().enabled = true;
            GameObject[] weapons = GameObject.FindGameObjectsWithTag("PlayerWeapon");
            foreach (GameObject currentWeapon in weapons)
            {
              currentWeapon.GetComponent<WeaponShooting>().fireRate/=2.0f;  
            }
            GetComponent<Player>().movementSpeed*=1.5f;
            currentSpecialLength+=specialLength;
            currentSpecialCD+=specialCD+specialLength;
        }
    }
    IEnumerator waitForSpecialCD(){
    allowSpecialCDDecrease = false;
        yield return new WaitForSeconds(1);
        currentSpecialCD--;
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    allowSpecialCDDecrease = true;
    }
    IEnumerator waitForSpecialLength(){
     allowSpecialLengthDecrease = false;          
    yield return new WaitForSeconds(1);
    currentSpecialLength--;  
    if(currentSpecialLength==0){
        transform.Find("GunslingerCowboyHat").GetComponent<SpriteRenderer>().enabled = false;
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("PlayerWeapon");
            foreach (GameObject currentWeapon in weapons)
        {
              currentWeapon.GetComponent<WeaponShooting>().fireRate*=2.0f;  
        }
        GetComponent<Player>().movementSpeed/=1.5f;
    }
    allowSpecialLengthDecrease = true;
    }
}
