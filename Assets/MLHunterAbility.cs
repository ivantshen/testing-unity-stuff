using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLHunterAbility : MonoBehaviour
{
    public int specialCD;
    public GameObject trap;
    public int trapDamage;
    public float trapSpeed;
    private int currentSpecialCD = 0;
    private bool allowSpecialCDDecrease = true;
    [SerializeField] private GameObject abilityBar;
    [SerializeField] private Transform playerBullets;
    // Update is called once per frame
    void Start(){
        abilityBar.SendMessage("assignAbilityMaxCooldown",specialCD);
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    }
    void Update()
    {
        if(currentSpecialCD>0&&allowSpecialCDDecrease){
            StartCoroutine(waitForSpecialCD());
        }
    }
    public void reset(){
        currentSpecialCD=0;
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    }
    public bool tryFire(){
        if(currentSpecialCD==0){
            Transform weapon = transform.Find("Weapon").transform;
            for(int i=0;i<6;i++){
            GameObject newBullet = Instantiate(trap,weapon.GetChild(i).transform.position,weapon.GetChild(i).transform.rotation,playerBullets) as GameObject;
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
            currentSpecialCD+=specialCD;
            return true;
        }
        return false;
    }
    IEnumerator waitForSpecialCD(){
    allowSpecialCDDecrease = false;
        yield return new WaitForSeconds(1);
        currentSpecialCD--;
        abilityBar.SendMessage("assignAbilityCooldown",currentSpecialCD);
    allowSpecialCDDecrease = true;
    }
}
