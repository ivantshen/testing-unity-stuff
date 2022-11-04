using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenToBullet : MonoBehaviour
{
    private Camera mainCam;
    public GameObject missile;
    public float timeToWait;
    public Slider warningSlider;
    public float updateFrequency;
    private bool allowCountDown = true;
    private float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        warningSlider = GetComponent<Slider>();
        warningSlider.maxValue = timeToWait;
        warningSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowCountDown&&timeToWait>0){
            StartCoroutine(countDown());
        }else if(currentTime>=timeToWait){
            GameObject newMissile = Instantiate(missile, mainCam.ScreenToWorldPoint(transform.position),transform.rotation,null);
            newMissile.transform.position = new Vector3(newMissile.transform.position.x,newMissile.transform.position.y,0);
            Destroy(gameObject);
        }
    }
    IEnumerator countDown(){
        allowCountDown = false;
        yield return new WaitForSeconds(updateFrequency);
        currentTime+=updateFrequency;
        warningSlider.value = currentTime;
        allowCountDown = true;
    }
}
