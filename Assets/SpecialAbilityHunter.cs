using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityHunter : MonoBehaviour
{
    public int specialCD;
    public GameObject trap;
    public int trapDamage;
    public float trapSpeed;
    private int currentSpecialCD = 0;
    private bool allowSpecialCDDecrease = true;
    // Update is called once per frame
    void Update()
    {
        if(currentSpecialCD>0&&allowSpecialCDDecrease){
            StartCoroutine(waitForSpecialCD());
        }
        if(Input.GetKeyDown("space")&&currentSpecialCD==0){
            for(int i=1;i<4;i++){
            GameObject newBullet = Instantiate(trap,transform.GetChild(i).transform.position,transform.GetChild(i).transform.rotation) as GameObject;
            Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(),GetComponent<Collider2D>());
            newBullet.SendMessage("assignDamage",trapDamage);
            newBullet.SendMessage("assignSpeed",trapSpeed);   
            }
            currentSpecialCD+=specialCD;
        }
    }
    IEnumerator waitForSpecialCD(){
    allowSpecialCDDecrease = false;
        yield return new WaitForSeconds(1);
        currentSpecialCD--;
    allowSpecialCDDecrease = true;
    }
}
