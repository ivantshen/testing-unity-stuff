using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeSpriteChangeOnDamage : MonoBehaviour
{
    private Sprite[] spritesForPhaseChange;
    private Stats stats;
    private SpriteRenderer sr;
    private float timeTillDeath = 25f;
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
        timeTillDeath-=Time.deltaTime;
        if(stats.health>=stats.maxHealth&&timeTillDeath>=20){
            sr.sprite = spritesForPhaseChange[0];
        }else if(stats.health>=stats.maxHealth*0.8f&&timeTillDeath>=15){
            sr.sprite = spritesForPhaseChange[1];
        }else if(stats.health>=stats.maxHealth*0.6f&&timeTillDeath>=10){
            sr.sprite = spritesForPhaseChange[2];
        }else if(stats.health>=stats.maxHealth*0.4f&&timeTillDeath>=5){
            sr.sprite = spritesForPhaseChange[3];
        }else{
            sr.sprite = spritesForPhaseChange[4];
            if(timeTillDeath<=0){
            Destroy(gameObject);
            }
        }
    }
}
