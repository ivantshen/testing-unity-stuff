using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NathanYuAI : MonoBehaviour
{ 
    public GameObject NathanYuHealthBar;
    public GameObject NathanYuSpeech;
    public Stats stats;
    public Rigidbody2D rb;
    private bool bossAwake = false;
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="GameBarrier"){
            ScreenShake.Instance.ShakeCamera(rb.gravityScale,0.5f);
            if(rb.gravityScale>0){
             rb.gravityScale--;  
             rb.velocity = Vector2.up *rb.gravityScale*2.5f; 
            }
            if(rb.gravityScale==0&&!bossAwake){
            Instantiate(NathanYuHealthBar,new Vector2(960f,260f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            Instantiate(NathanYuSpeech,new Vector2(960f,260f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            bossAwake = true;
            }
        }
    }
}
