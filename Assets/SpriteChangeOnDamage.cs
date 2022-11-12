using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangeOnDamage : MonoBehaviour
{
    private Sprite[] spritesForPhaseChange;
    private Stats stats;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        stats = GetComponent<Stats>();
        spritesForPhaseChange = Resources.LoadAll<Sprite>("Spritesheets/barricadeSpriteSheet");
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.health>=stats.maxHealth){
            sr.sprite = spritesForPhaseChange[0];
        }else if(stats.health>=stats.maxHealth*0.8f){
            sr.sprite = spritesForPhaseChange[1];
        }else if(stats.health>=stats.maxHealth*0.6f){
            sr.sprite = spritesForPhaseChange[2];
        }else if(stats.health>=stats.maxHealth*0.4f){
            sr.sprite = spritesForPhaseChange[3];
        }else{
            sr.sprite = spritesForPhaseChange[4];
        }
    }
}
