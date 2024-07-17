using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelixPengMoneyFire : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets; //1,5,10,20,50,100,500
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject felixPengUI;
    private Transform canvas;
    private FelixPengMoneyControl fpmc;
    private bool allowFire = true;
    private bool autoFiring = false;
    private bool stunned = false;
    // Update is called once per frame
    void Start(){
        canvas = GameObject.FindWithTag("MainCanvas").transform;
        fpmc = Instantiate(felixPengUI,new Vector2(0f,0f), Quaternion.identity,canvas).GetComponent<FelixPengMoneyControl>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            autoFiring = !autoFiring;
        }
        if((Input.GetKey(KeyCode.Mouse0)||autoFiring)&&allowFire&&!stunned){
            StartCoroutine(generateBullet());
        }
    }
    IEnumerator generateBullet(){
        int totalMoney = 0;
        allowFire = false;
        int numBullets = Random.Range(1,12);
        for(int i=0;i<numBullets;i++){
            int randChance = Random.Range(1,100);
            int index = 0;
            if(randChance==1){
                index=6;
                totalMoney+=500;
            }else if(randChance<=6){
                index=5;
                totalMoney+=100;
            }else if(randChance<=16){
                index=4;
                totalMoney+=50;
            }else if(randChance<=41){
                index=3;
                totalMoney+=20;
            }else if(randChance<=71){
                index=2;
                totalMoney+=10;
            }else if(randChance<=91){
                index=1;
                totalMoney+=5;
            }else{
                index=0;
                totalMoney+=1;
            }
            GameObject newBullet = Instantiate(bullets[index],transform.GetChild(i).position,transform.GetChild(i).rotation) as GameObject;
        }
            fpmc.firedMoney(totalMoney);
            yield return new WaitForSeconds(fireRate);
            allowFire = true;
    }
    public void stun(bool stun){
        stunned = stun;
    }
}
