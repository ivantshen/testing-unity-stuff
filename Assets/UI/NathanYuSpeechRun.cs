using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NathanYuSpeechRun : MonoBehaviour
{
    public TMP_Text nathanSpeechTextArea;
    int currentText = 0;
    public bool currentlyTalking = false;
    void Start(){
        StartCoroutine(displayText("So you've found me..."));
    }
    void Update(){
        if(currentText==1&&!currentlyTalking){
          StartCoroutine(displayText("Even if you best me, you still must fell the mighty Andrew Huang."));  
        }else if(currentText==2&&!currentlyTalking){
        StartCoroutine(displayText("You can stop while you're ahead and save yourself the trouble."));  
        }else if(currentText==3&&!currentlyTalking){
            StartCoroutine(waitAndDestroy());
        }
    }
    IEnumerator waitAndDestroy(){
        currentlyTalking = true;
        yield return new WaitForSeconds(5);
         Destroy(gameObject);
    }
    IEnumerator displayText(string text){
        currentlyTalking = true;
        WaitForSeconds wait = new WaitForSeconds(0.075f);
        for(int i=0;i<text.Length;i++){
            nathanSpeechTextArea.text+= text[i];
            if(i==text.Length-1){
                nathanSpeechTextArea.text+="\n";
                currentText++;
            }
            if(!text[i].Equals(" ")){
             yield return wait;   
            }
            
        }
        currentlyTalking = false;
    }
}
