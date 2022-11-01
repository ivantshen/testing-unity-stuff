using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int bulletDamage;
    public float bulletSpeed;
    public GameObject bullet;
    public float fireRate;
    private bool allowFire = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)&&allowFire){
            StartCoroutine(generateBullet());
        }
    }
    IEnumerator generateBullet(){
        allowFire = false;
            GameObject newBullet = Instantiate(bullet,transform.GetChild(1).transform.position,transform.GetChild(1).transform.rotation) as GameObject;
            Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(),GetComponent<Collider2D>());
            newBullet.SendMessage("assignDamage",bulletDamage);
            newBullet.SendMessage("assignSpeed",bulletSpeed);
            yield return new WaitForSeconds(fireRate);
            allowFire = true;
    }
}
