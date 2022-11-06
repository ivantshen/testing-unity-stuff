using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance  {get;private set;}
    private CinemachineVirtualCamera cinemachineVCam;
    private float shakeTimer;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cinemachineVCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float duration){
        CinemachineBasicMultiChannelPerlin cinemachineBscMultChanPer = cinemachineVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBscMultChanPer.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }
    private void Update(){
        if(shakeTimer>0){
            shakeTimer-=Time.deltaTime;
            if(shakeTimer<=0f){
                CinemachineBasicMultiChannelPerlin cinemachineBscMultChanPer = cinemachineVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBscMultChanPer.m_AmplitudeGain = 0f;
            }
        } 
    }
}
