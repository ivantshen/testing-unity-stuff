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
    private UpdateAbilityBar abilityBar;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start(){
        abilityBar = GameObject.FindWithTag("AbilityBar").GetComponent<UpdateAbilityBar>();
        abilityBar.assignAbilityMaxCooldown(specialCD);
    }
    void Update()
    {
        if(abilityBar.abilityIsReady()&&Input.GetKeyDown("space")){
            abilityBar.usedAbility();
            ScreenShake.Instance.ShakeCamera(8f,0.75f);
            Transform weapon = transform.Find("Weapon").transform;
            Instantiate(barricade,weapon.GetChild(0).transform.position,weapon.rotation);
            GameObject newSentry = Instantiate(sentry,weapon.GetChild(0).transform.position,weapon.rotation) as GameObject;
            newSentry.SendMessage("assignDamage",sentryDamage);
            newSentry.SendMessage("assignFireRate",sentryFireRate);   
            newSentry.SendMessage("assignHealth",sentryHealth);
            newSentry.SendMessage("assignBulletSpeed",sentryBulletSpeed);
            newSentry.SendMessage("assignReactionTime",sentryReactionTime);
            newSentry.SendMessage("assignDeathTime",sentryLifeDuration);
        }
    }
}
