using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTOJAIL : MonoBehaviour
{
    [SerializeField] private GameObject JAIL;
    void OnTriggerEnter2D(Collider2D other){
        ScreenShake.Instance.ShakeCamera(16f,1.5f);
        Instantiate(JAIL,transform.position,Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}
