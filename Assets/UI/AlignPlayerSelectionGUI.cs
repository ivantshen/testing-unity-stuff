using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignPlayerSelectionGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).position = new Vector2(Screen.width*0.25f,Screen.height*0.65f);
        transform.GetChild(1).position = new Vector2(Screen.width*0.5f,Screen.height*0.65f);
        transform.GetChild(2).position = new Vector2(Screen.width*0.75f,Screen.height*0.65f);
        transform.GetChild(3).position = new Vector2(Screen.width*0.5f,Screen.height*0.35f);
        transform.GetChild(4).position = new Vector2(Screen.width*0.5f,Screen.height*0.1f);
    }

}
