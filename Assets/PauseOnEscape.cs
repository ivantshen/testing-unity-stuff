using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOnEscape : MonoBehaviour
{
    private bool paused = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            if(paused){
                Time.timeScale = 0;
                GameObject.FindWithTag("BackgroundCanvas").GetComponent<Image>().enabled = true;
                GameObject.FindWithTag("BackgroundCanvas").GetComponent<Image>().color = new Color(135,135,135,0.3f);
            }else{
                Time.timeScale = 1;
                GameObject.FindWithTag("BackgroundCanvas").GetComponent<Image>().enabled = false;
                GameObject.FindWithTag("BackgroundCanvas").GetComponent<Image>().color = new Color(135,135,135,1f);
            }
        }
    }
}
