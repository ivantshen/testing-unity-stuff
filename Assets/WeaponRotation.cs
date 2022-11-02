using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject player;
    public float extraDegreesToRotate;
    void Start(){
        mainCamera = Camera.main;
    }
    private void FixedUpdate() {
        Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
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
