using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityFelix : MonoBehaviour
{
    public int specialCD;
    [SerializeField] private GameObject vault;
    [SerializeField] private FelixPengMoneyFire fpmf;
    [SerializeField] private Stats felixStat;
    private Collider2D col;
    private UpdateAbilityBar abilityBar;
    private bool abilityEnabled = true;
    private float ms;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start(){
        abilityBar = GameObject.FindWithTag("AbilityBar").GetComponent<UpdateAbilityBar>();
        abilityBar.assignAbilityMaxCooldown(specialCD);
        ms = felixStat.movementSpeed;
        col = GetComponent<Collider2D>();
    }
    void Update()
    {
        if(abilityBar.abilityIsReady()&&Input.GetKeyDown("space")){
            felixStat.movementSpeed = 0;
            abilityBar.usedAbility();
            abilityBar.freezeTheCD(true);
            ScreenShake.Instance.ShakeCamera(8f,0.75f);
            GameObject felixVault = Instantiate(vault,transform.position,Quaternion.identity);
            felixVault.GetComponent<FelixVaultScript>().setAbility(this);
            fpmf.stun(true);
            felixStat.invincible = true;
            col.enabled = false;
        }
    }
    public void vaultBroken(){
        fpmf.stun(false);
        felixStat.movementSpeed = ms;
        felixStat.invincible=false;
        abilityBar.freezeTheCD(false);
        col.enabled = true;
    }
}
