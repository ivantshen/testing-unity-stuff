using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MLScreenToBullet : MonoBehaviour
{
    public GameObject missile;
    public float timeToWait;
    public Slider warningSlider;
    public float updateFrequency;
    private float currentTime = 0;
    private Transform enemyBullets;
    // Start is called before the first frame update
    void Start()
    {
        warningSlider = GetComponent<Slider>();
        warningSlider.maxValue = timeToWait;
        warningSlider.value = 0;
    }
    public void setEnemyBullets(Transform eb){
        enemyBullets = eb;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime+=Time.deltaTime;
        warningSlider.value = currentTime;
        if(currentTime>=timeToWait){
            GameObject newMissile = Instantiate(missile, gameObject.transform.position,transform.rotation,enemyBullets);
            newMissile.transform.position = new Vector3(newMissile.transform.position.x,newMissile.transform.position.y,0);
            Destroy(gameObject);
        }
    }
}
