using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alignDeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         transform.GetChild(0).position = new Vector2(Screen.width*0.5f,Screen.height*0.75f);
        transform.GetChild(1).position = new Vector2(Screen.width*0.5f,Screen.height*0.4f);
    }
}
