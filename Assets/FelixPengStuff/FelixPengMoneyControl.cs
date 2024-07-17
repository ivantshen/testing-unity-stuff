using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FelixPengMoneyControl : MonoBehaviour
{
    private int[] moneyFromLastThreeFires = new int[3];
    [SerializeField] private TMP_Text[] moneyTexts; //first,second,third,total
    [SerializeField] private GameObject jailSquares;
    private Transform player;
    private Camera cam;
    private float totalSum;
    private float currentTotalDisplay;
    private int nextTextToChange = 0;
    private float lastMoneyDifference = 0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player){
        moneyTexts[0].transform.position =cam.WorldToScreenPoint(new Vector2(player.position.x-1.5f,player.position.y+.5f));
        moneyTexts[1].transform.position =cam.WorldToScreenPoint(new Vector2(player.position.x+1.5f,player.position.y+.5f));
        moneyTexts[2].transform.position =cam.WorldToScreenPoint(new Vector2(player.position.x,player.position.y+2.5f));
        moneyTexts[3].transform.position =cam.WorldToScreenPoint(new Vector2(player.position.x,player.position.y+1.5f));
        if(currentTotalDisplay<totalSum){
            currentTotalDisplay+=Time.deltaTime*(lastMoneyDifference/1.5f);
            moneyTexts[3].text = "$"+((int)(currentTotalDisplay)).ToString();
            if(currentTotalDisplay>250&&currentTotalDisplay<750){
            moneyTexts[3].color = new Color(1f,1f-(currentTotalDisplay-250)*.002f,1f-(currentTotalDisplay-250)*.002f,1f);
            }
        }else{
            moneyTexts[3].text = "$"+((int)(totalSum)).ToString();
            if(totalSum>750){
                moneyTexts[3].color = new Color(1f,0f,0f,1f);
            }else if(totalSum>250){
                moneyTexts[3].color = new Color(1f,1f-(totalSum-250)*0.002f,1f-(totalSum-250)*0.002f,1f);
            }
        }    
        }
        
    }
    public void firedMoney(int amt){
        moneyTexts[nextTextToChange].text = "$"+amt.ToString();   
        if(amt>250){
            moneyTexts[nextTextToChange].color = new Color(0.8f,0f,0f,1f);
        }
        lastMoneyDifference = amt;
        totalSum+=amt;
        nextTextToChange++;
        if(nextTextToChange==3){
            StartCoroutine(checkForJail());
        }
    }
    IEnumerator checkForJail(){
        yield return new WaitForSeconds(1.5f);
        if(totalSum>750){
            ScreenShake.Instance.ShakeCamera(8f,0.75f);
            Instantiate(jailSquares,player.position,Quaternion.identity);
        }
        nextTextToChange=0;
        moneyTexts[0].text = "$0";
        moneyTexts[1].text = "$0";
        moneyTexts[2].text = "$0";
        moneyTexts[0].color = new Color(1f,1f,1f,1f);
        moneyTexts[1].color = new Color(1f,1f,1f,1f);
        moneyTexts[2].color = new Color(1f,1f,1f,1f);
        totalSum=0;
        currentTotalDisplay=0;
        moneyTexts[3].text= "$0";
        moneyTexts[3].color = new Color(1f,1f,1f,1f);
    }
}
