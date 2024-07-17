using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MLTarget : MonoBehaviour
{
    private float spitCD = 0.85f;
    private float spitTimer = 0f;
    private GameObject player;
    [SerializeField] private Transform firepoint1;
    [SerializeField] private GameObject spitball;
    private Transform enemyBullets;
    private float spitSpeed = 3.35f;
    // Update is called once per frame
    void Update()
    {
            Vector3 difference = player.transform.localPosition-transform.localPosition;
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,0f,rotationZ);  
            if(rotationZ <-90||rotationZ>90){
                if(player.transform.eulerAngles.y==0){
                    transform.localRotation = Quaternion.Euler(180,0,-rotationZ);
                }else if(player.transform.eulerAngles.y==180){
                    transform.localRotation = Quaternion.Euler(180,180,-rotationZ);
                }
        }   
        
        spitTimer+=Time.deltaTime;
        if(spitTimer>=spitCD){
            spitTimer=0f;
            Quaternion offset = Quaternion.AngleAxis(Random.Range(-4,5), Vector3.forward);
            GameObject newSpitBall =Instantiate(spitball,firepoint1.position,(firepoint1.rotation*offset),enemyBullets);
            EnemySlowingBulletMotion temp = newSpitBall.GetComponent<EnemySlowingBulletMotion>();
            temp.assignSpeed(spitSpeed);
            temp.assignDamage(15);
        }
        
    }
    public void setUp(GameObject p,Transform eb){
            player=p;
            enemyBullets = eb;
            p.GetComponent<MLStats>().invincible = false;
        }
}
