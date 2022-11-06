using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdateBossHp : MonoBehaviour
{
    public TMP_Text healthBarDisplay;
    public Slider healthBar;
    private Stats boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss").GetComponent<Stats>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = boss.maxHealth;
        healthBar.value = boss.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.health<=0){
            Destroy(gameObject);
        }else{
            healthBarDisplay.text = (boss.health.ToString()+"/"+boss.maxHealth.ToString());
        }
        healthBar.value = boss.health;
    }
}
