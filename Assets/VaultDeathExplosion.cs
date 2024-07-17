using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDeathExplosion : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private int numBullets;
    private float deathTime = 7f;
    private float timer;
    private Vector2 pos;
    void Start(){
        pos = transform.position;
    }
    void Update(){
        if(timer<deathTime){
            timer+=Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void OnDestroy(){
        for(int i=0;i<numBullets;i++){
            int randChance = Random.Range(1,100);
            int index = 0;
            if(randChance==1){
                index=6;
            }else if(randChance<=6){
                index=5;
            }else if(randChance<=16){
                index=4;
            }else if(randChance<=41){
                index=3;
            }else if(randChance<=71){
                index=2;
            }else if(randChance<=91){
                index=1;
            }else{
                index=0;
            }
            float randAngle = Random.Range(0f,360f);
            Instantiate(bullets[index],pos,Quaternion.Euler(0f,0f,randAngle));
        }
    }
}
