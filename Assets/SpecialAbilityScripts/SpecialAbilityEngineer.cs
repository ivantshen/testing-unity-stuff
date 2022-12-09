using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityEngineer : MonoBehaviour
{
    public int specialCD;
    public GameObject sentry;
    public GameObject barricade;
    public int sentryDamage;
    public float sentryFireRate;
    public int sentryHealth;
    public float sentryReactionTime;
    public float sentryBulletSpeed;
    public int sentryLifeDuration;
    private int currentSpecialCD = 0;
    private bool allowSpecialCDDecrease = true;
    private GameObject abilityBar;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start(){
        abilityBar = GameObject.FindWithTag("AbilityBar");
        abilityBar.SendMessage("assignAbilityMaxCooldown",specialCD);
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    }
    void Update()
    {
        if(currentSpecialCD>0&&allowSpecialCDDecrease){
            StartCoroutine(waitForSpecialCD());
        }
        if(Input.GetKeyDown("space")&&currentSpecialCD==0){
            Transform weapon = transform.Find("Weapon").transform;
            Instantiate(barricade,weapon.GetChild(0).transform.position,weapon.rotation);
            GameObject newSentry = Instantiate(sentry,weapon.GetChild(0).transform.position,weapon.rotation) as GameObject;
            newSentry.SendMessage("assignDamage",sentryDamage);
            newSentry.SendMessage("assignFireRate",sentryFireRate);   
            newSentry.SendMessage("assignHealth",sentryHealth);
            newSentry.SendMessage("assignBulletSpeed",sentryBulletSpeed);
            newSentry.SendMessage("assignReactionTime",sentryReactionTime);
            newSentry.SendMessage("assignDeathTime",sentryLifeDuration);
            currentSpecialCD+=specialCD;
        }
    }
     IEnumerator waitForSpecialCD(){
    allowSpecialCDDecrease = false;
        yield return new WaitForSeconds(1);
        currentSpecialCD--;
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    allowSpecialCDDecrease = true;
    }
}
