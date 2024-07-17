using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public int bulletDamage;
    public int deathTime;
    public float bulletSpeed;
    public GameObject bullet;
    public int numBullets;
    public float delayBetweenBullets =0f;
    public float fireRate;
    private bool allowFire = true;
    [SerializeField] private float randomAngleOffset = 0;

    // Update is called once per frame
    void Update()
    {
        if(allowFire){
        StartCoroutine(generateBullet());
        }
    }
    IEnumerator generateBullet(){
        allowFire = false;
        for(int i=0;i<numBullets;i++){
          Quaternion offset = Quaternion.AngleAxis(Random.Range(-randomAngleOffset,randomAngleOffset), Vector3.forward);
          GameObject newBullet = Instantiate(bullet,transform.GetChild(i).transform.position,transform.GetChild(i).rotation*offset) as GameObject;
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            if(deathTime!=0){
              newBullet.SendMessage("assignDeathTime",deathTime);
            }
            yield return new WaitForSeconds(delayBetweenBullets);
        }
          yield return new WaitForSeconds(fireRate);    
            allowFire = true;
    }
}
