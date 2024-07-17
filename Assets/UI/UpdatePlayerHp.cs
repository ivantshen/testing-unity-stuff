using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdatePlayerHp : MonoBehaviour
{
    public TMP_Text healthBarDisplay;
    public Slider healthBar;
    private Stats player;

    // Update is called once per frame
    void Update()
    {
        if(player){
        if(player.health<=0){
            healthBarDisplay.text = "RIP";
        }else{
            healthBarDisplay.text = player.health.ToString();
        }
        healthBar.value = player.health;    
        }
    }
    public void delayedStart(){
        player = GameObject.FindWithTag("Player").GetComponent<Stats>();
        if(player){
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = player.maxHealth;
        healthBar.value = player.maxHealth;    
        }
    }
}
