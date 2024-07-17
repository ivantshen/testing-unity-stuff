using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLWeaponRotation : MonoBehaviour
{
    public GameObject player;
    public float extraDegreesToRotate;
    public bool allowRotate = true;
    public void setAngle(float rotationZ){
        transform.rotation = Quaternion.Euler(0f,0f,rotationZ +extraDegreesToRotate);
        if(rotationZ <-90||rotationZ>90){
            if(player.transform.eulerAngles.y==0){
                transform.localRotation = Quaternion.Euler(180,0,-rotationZ +extraDegreesToRotate);
            }else if(player.transform.eulerAngles.y==180){
                transform.localRotation = Quaternion.Euler(180,180,-rotationZ+extraDegreesToRotate);
            }
        }   
    }
}
