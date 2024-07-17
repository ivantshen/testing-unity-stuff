using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelixVaultScript : MonoBehaviour
{
    private SpecialAbilityFelix ability;
    void OnDestroy(){
        ability.vaultBroken();
    }
    public void setAbility(SpecialAbilityFelix a){
        ability = a;
    }
}
