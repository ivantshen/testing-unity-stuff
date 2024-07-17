using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityHunter : MonoBehaviour
{
    public int specialCD;
    public GameObject trap;
    public int trapDamage;
    public float trapSpeed;
    private UpdateAbilityBar abilityBar;
    // Update is called once per frame
    void Start(){
        abilityBar = GameObject.FindWithTag("AbilityBar").GetComponent<UpdateAbilityBar>();
        abilityBar.assignAbilityMaxCooldown(specialCD);
    }
    void Update()
    {
        if(Input.GetKeyDown("space")&&abilityBar.abilityIsReady()){
            abilityBar.usedAbility();
            ScreenShake.Instance.ShakeCamera(8f,0.75f);
            Transform weapon = transform.Find("Weapon").transform;
            for(int i=0;i<6;i++){
            GameObject newBullet = Instantiate(trap,weapon.GetChild(i).transform.position,weapon.GetChild(i).transform.rotation) as GameObject;
            newBullet.SendMessage("assignDamage",trapDamage);
            if(i<=1){
                newBullet.SendMessage("assignSpeed",trapSpeed*.7);
            }else{
                newBullet.SendMessage("assignSpeed",trapSpeed);
            }
            if(i%2==0&&i!=0){
                newBullet.SendMessage("assignRotateRight",true);
            }else if(i%2==1&&i!=1){
                newBullet.SendMessage("assignRotateLeft",true);
            }
            }
        }
    }
}
