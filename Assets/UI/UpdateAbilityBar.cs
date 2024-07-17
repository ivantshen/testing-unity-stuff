using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdateAbilityBar : MonoBehaviour
{
    public TMP_Text abilityBarDisplay;
    public Slider abilityBar;
    private float maxCoolDown;
    private float currentCD;
    private bool freezeCD = false;
    // Start is called before the first frame update
    public void assignAbilityMaxCooldown(float maxCD){
        abilityBar.maxValue = maxCD;
        currentCD=maxCD;
        abilityBar.value = currentCD;
        maxCoolDown = maxCD;
    }
    void Update(){
        if(!freezeCD){
            if(currentCD<maxCoolDown){
                currentCD+=Time.deltaTime;
                abilityBar.value = currentCD;
                float cdNum = ((int)((maxCoolDown-currentCD)*10f))/10f;
                abilityBarDisplay.text = "Ready In: "+ cdNum;
            }else{
                currentCD=maxCoolDown;
                abilityBarDisplay.text = "READY";
            }    
        }
        

    }
    public void usedAbility(){
        currentCD=0;
        abilityBar.value=0;
        abilityBarDisplay.text = "Ready In: "+ maxCoolDown;
}
    public bool abilityIsReady(){
        if(maxCoolDown==currentCD){
            return true;
        }else{
            return false;
        }
    }
    public void freezeTheCD(bool freeze){
        freezeCD = freeze;
    }
}
