using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeOnCreation : MonoBehaviour
{
    public float magnitude;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        ScreenShake.Instance.ShakeCamera(magnitude,duration);
    }

}
