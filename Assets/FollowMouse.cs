using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCam;
    void Start(){
        mainCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
