using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdatePlayerHp : MonoBehaviour
{
    public TMP_Text healthBarDisplay;
    public Slider healthBar;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject currentPlayer in allPlayers)
        {
            if(currentPlayer.GetComponent<Player>()!=null){
                player = currentPlayer.GetComponent<Player>();
            }
        }
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = player.maxHealth;
        healthBar.value = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.health<=0){
            healthBarDisplay.text = "RIP";
        }else{
            healthBarDisplay.text = player.health.ToString();
        }
        healthBar.value = player.health;
    }
}
