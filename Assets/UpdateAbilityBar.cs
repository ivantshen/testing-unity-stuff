using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdateAbilityBar : MonoBehaviour
{
    public TMP_Text abilityBarDisplay;
    public Slider abilityBar;
    private int maxCoolDown;
    // Start is called before the first frame update
    void assignAbilityMaxCooldown(int maxCD){
        abilityBar.maxValue = maxCD;
        abilityBar.value = maxCD;
        maxCoolDown = maxCD;
    }
    void assignAbilityCooldown(int cd){
        abilityBar.value = maxCoolDown-cd;
        if(cd <=0){
            abilityBarDisplay.text = "READY";
        }else{
         abilityBarDisplay.text = "Ready In: "+cd.ToString();   
        }
        
    }
}
