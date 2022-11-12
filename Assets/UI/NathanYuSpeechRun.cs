using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NathanYuSpeechRun : MonoBehaviour
{
    public TMP_Text nathanSpeechTextArea;
    private Stats nathanStats;
    int currentText = 0;
    int enragedText =0;
    public bool currentlyTalking = false;
    public bool hasPhaseChanged = false;
    private IEnumerator currentTextCoroutine;
    void Start(){
        nathanStats = GameObject.FindWithTag("Boss").GetComponent<Stats>();
        currentTextCoroutine =displayText("So you've found me...");
        StartCoroutine(currentTextCoroutine);
    }
    void Update(){
        if(nathanStats.health>9500){
        if(currentText==1&&!currentlyTalking){
          currentTextCoroutine=displayText("Even if you best me, you still must fell the mighty Andrew Huang.");
          StartCoroutine(currentTextCoroutine);  
        }else if(currentText==2&&!currentlyTalking){
            currentTextCoroutine=displayText("You can stop while you're ahead and save yourself the trouble.");
        StartCoroutine(currentTextCoroutine);  
        }else if(currentText==3&&!currentlyTalking){
            currentTextCoroutine = waitAndDestroy();
            StartCoroutine(currentTextCoroutine);
        }    
        }else{
            currentText=-1;
            if(!hasPhaseChanged){
                hasPhaseChanged = true;
                nathanStats.invincible = true;
                nathanSpeechTextArea.text = "";
                StopCoroutine(currentTextCoroutine);
                StartCoroutine(displayText("DO NOT INTERRUPT ME!"));
                ScreenShake.Instance.ShakeCamera(6f,2.5f);
            }else if(enragedText==1&&!currentlyTalking){
                StartCoroutine(displayText("IT SEEMS YOU HAVE A LACK OF SELF PRESERVATION"));
            }else if(enragedText==2&&!currentlyTalking){
                StartCoroutine(displayText("YOU AREN'T EVEN WORTH MY TIME!"));
            }else if(enragedText==3&&!currentlyTalking){
                nathanStats.invincible = false;
                 StartCoroutine(waitAndDestroy());
            }
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
                if(currentText>-1){
                 currentText++;   
                }else{
                    enragedText++;
                }
                
            }
            if(!text[i].Equals(" ")){
             yield return wait;   
            }
            
        }
        currentlyTalking = false;
    }
}
