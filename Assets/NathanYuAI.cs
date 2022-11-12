using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NathanYuAI : MonoBehaviour
{ 
    public GameObject NathanYuHealthBar;
    public GameObject NathanYuSpeech;
    public GameObject warning;
    public Sprite enragedPhase0;
    private GameObject instantiatedHealthBar;
    // 0 = spin emitter 1 and 2 = split up map 3 = spitball 4 = explosion
    public GameObject[] moves;
    public Stats stats;
    public Rigidbody2D rb;
    private int currentSizeIncrease =0;
    private bool enraged = false;
    private bool bossAwake = false;
    private int currentPhase = 0;
    private bool allowMoves = true;
    private bool allowTracking = true;
    private bool stopTracking = false;
    private bool allowCollisionDamage = false;
    private bool bossChasing = false;
    private bool bossSpitting = true;
    private bool stopSpitting = true;
    private float spitSpeed = 0f;
    private float phase1MoveChainSpeed = 10f;
    private float phase1StaggerLength =7f;
    private float spitCD = 0.85f;
    private Transform firepoint1;
    private IEnumerator currentCoroutine;
    private Camera mainCam;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6,9);
        firepoint1 = transform.GetChild(0).transform;
        mainCam = Camera.main;
        rb.gravityScale = 8f;
        stats.invincible = true;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(bossAwake){
            if(currentPhase==0&&allowMoves){
                currentCoroutine = phase0Transition();
                StartCoroutine(currentCoroutine);
            }
            if(currentPhase==0&&stats.health<=9500&&!enraged){
                enraged = true;
                phase1StaggerLength -=1f;
                phase1MoveChainSpeed-=1.5f;
                spitCD -=0.125f;
                StopCoroutine(currentCoroutine);
                StartCoroutine(phase0EnragedTransition());
            }
            if(currentPhase==1){
                if(allowTracking&&!stopTracking){
                    StartCoroutine(waitToTrack());
                }
                if(allowMoves){
                    currentCoroutine = phase1MoveChaining();
                    StartCoroutine(currentCoroutine);
                }
                if(bossChasing&&player!=null){
                    float step = stats.movementSpeed *Time.deltaTime;
                     transform.position = Vector2.MoveTowards(transform.position,player.transform.position,step);
                }
            }
            if(bossSpitting&&!stopSpitting){
                StartCoroutine(spit());
            }
            
        }
    }
    IEnumerator spit(){
        bossSpitting = false;
        Quaternion offset = Quaternion.AngleAxis(Random.Range(-15,16), Vector3.forward);
        GameObject newSpitBall =Instantiate(moves[3],firepoint1.position,firepoint1.rotation*offset);
        newSpitBall.SendMessage("assignDamage",15);
        newSpitBall.SendMessage("assignSpeed",spitSpeed);
        yield return new WaitForSeconds(spitCD);
        bossSpitting = true;
    }
    IEnumerator waitToTrack(){
        allowTracking=false;
        if(player!=null){
         Vector3 difference = player.transform.position-transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
          transform.rotation = Quaternion.Euler(0f,0f,rotationZ);  
        if(rotationZ <-90||rotationZ>90){
            if(player.transform.eulerAngles.y==0){
                transform.localRotation = Quaternion.Euler(180,0,-rotationZ);
            }else if(player.transform.eulerAngles.y==180){
                transform.localRotation = Quaternion.Euler(180,180,-rotationZ);
            }
        }   
        }
        yield return new WaitForSeconds(0.05f);
        allowTracking = true;
    }
    IEnumerator phase1MoveChaining(){
        allowMoves = false;
        int move1Selection = Random.Range(1,4);
        int move2Selection = Random.Range(1,4);
        while(move1Selection ==move2Selection){
            move2Selection = Random.Range(1,4);
        }
        if(move1Selection==1){
            phase1SpinnerPlacement();
            bossChasing = true;
        }else if(move1Selection==2){
           phase1DivideMap();
            spitSpeed = 3.35f;
            Instantiate(warning,new Vector2(960f,540f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            yield return new WaitForSeconds(0.85f);
            stopSpitting = false;
            transform.position = new Vector2(0,0);
        }else{
            phase1ExplosionPlacement();
            bossChasing = true;
            stopSpitting = false;
            stats.speedChangePercent(-0.15f,9.5f);
            spitSpeed = 4.15f;
        }
        allowCollisionDamage = true;
        yield return new WaitForSeconds(phase1MoveChainSpeed);
        stopSpitting = true;
        bossChasing = false;
        allowCollisionDamage = false;
        if(move2Selection==1){
            phase1SpinnerPlacement();
            bossChasing = true;
        }else if(move2Selection==2){
            phase1DivideMap();
            spitSpeed = 3.35f;
            Instantiate(warning,new Vector2(960f,540f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            yield return new WaitForSeconds(0.85f);
            stopSpitting = false;
            transform.position = new Vector2(0,0);
            
        }else{
            phase1ExplosionPlacement();
            bossChasing = true;
            spitSpeed = 4.15f;
            stats.speedChangePercent(-0.15f,9.5f);
            stopSpitting = false;
        }
        allowCollisionDamage = true;
        yield return new WaitForSeconds(phase1MoveChainSpeed);
        stopSpitting = true;
        bossChasing = false;
        allowCollisionDamage = false;
        stagger(true);
        yield return new WaitForSeconds(phase1StaggerLength);
        stagger(false);
        allowMoves = true;
    }
    private void stagger(bool TorF){
        if(TorF){
        stats.changeDamageTakenMultiplier(0.5f);
        //Out of bounds check
        if(transform.position.y<-7f){
        transform.position = new Vector3(transform.position.x,-7f,0f);
        }else if(transform.position.y>7){
        transform.position = new Vector3(transform.position.x,7f,0f);
        }
        if(transform.position.x>16.5f){
        transform.position = new Vector3(16.5f,transform.position.y,0f);
        }else if(transform.position.x<-16.5f){
          transform.position = new Vector3(-16.5f,transform.position.y,0f);  
        }
        rb.gravityScale+=3;   
        stopTracking = true;
        rb.rotation=Random.Range(-75.0f,75.0f);

        }else{
        rb.rotation = 0f;
        stats.changeDamageTakenMultiplier(-0.5f);
        stopTracking = false;
        }
    }
    private void phase1DivideMap(){
        int decision = Random.Range(1,3);
        GameObject divider = moves[decision];
        if(decision==2){
        Instantiate(divider,new Vector2(0f,0f),Quaternion.identity);
        }else{
        Instantiate(divider,new Vector2(0f,0f),Quaternion.Euler(0,0,45));
        }
        
    }
    private void phase1SpinnerPlacement(){
        int amountOfPlacements = 6;
        GameObject emitter = moves[0];
        if(player!=null){
        Instantiate(emitter,mainCam.WorldToScreenPoint(player.transform.position),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        amountOfPlacements--;    
        }
        while(amountOfPlacements>0){
            amountOfPlacements--;
            Instantiate(emitter,new Vector2(Random.Range(150f,1770f),Random.Range(100f,980f)),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        }
    }
    private void phase1ExplosionPlacement(){
        int amountOfPlacements = 7;
        GameObject explosion = moves[4];
        if(player!=null){
        Instantiate(explosion,mainCam.WorldToScreenPoint(player.transform.position+new Vector3(Random.Range(-1.2f,1.2f),Random.Range(-1.2f,1.2f),0f)),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        amountOfPlacements--;    
        }
        GameObject[] allSentries = GameObject.FindGameObjectsWithTag("Sentry");
        foreach (GameObject currentSentry in allSentries)
        {
            if(amountOfPlacements>0){
            Instantiate(explosion,mainCam.WorldToScreenPoint(currentSentry.transform.position+new Vector3(Random.Range(-0.8f,0.8f),Random.Range(-0.8f,0.8f),0f)),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            Instantiate(explosion,mainCam.WorldToScreenPoint(currentSentry.transform.position+new Vector3(Random.Range(-0.8f,0.8f),Random.Range(-0.8f,0.8f),0f)),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            amountOfPlacements-=2;
            }
        }
        while(amountOfPlacements>0){
            amountOfPlacements--;
            Instantiate(explosion,new Vector2(Random.Range(150f,1770f),Random.Range(100f,980f)),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        }
    }
    IEnumerator phase0Transition(){
        allowMoves = false;
        yield return new WaitForSeconds (3f);
        rb.velocity = Vector2.up *1.55f;
        yield return new WaitForSeconds(6.5f);
        rb.velocity = new Vector2(0f,0f);
        for(int i=1;i<7;i++){
            currentSizeIncrease ++;
          transform.localScale += new Vector3(0.5f,0.5f,0f);  
          ScreenShake.Instance.ShakeCamera((2f*i),0.615f);
          yield return new WaitForSeconds(1.25f);
        }
        instantiatedHealthBar.SetActive(true);
        currentPhase++;
        allowMoves = true;
    }
    IEnumerator phase0EnragedTransition(){
        allowMoves= false;
        rb.velocity = new Vector2(0f,0f);
        GetComponent<SpriteRenderer>().sprite = enragedPhase0;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        for(float i=1;i<5;i++){
            GetComponent<SpriteRenderer>().color = new Color(1f,1f-(0.15f*i),1f-(0.15f*i),1f);
            ScreenShake.Instance.ShakeCamera(3f,0.615f);
            yield return new WaitForSeconds(1.25f);
        }
        for(int i=currentSizeIncrease+1;i<7;i++){
          transform.localScale += new Vector3(0.5f,0.5f,0f);  
          ScreenShake.Instance.ShakeCamera((3f*i),0.615f);
          yield return new WaitForSeconds(1.25f);
        }
        yield return new WaitForSeconds(1.25f);
        instantiatedHealthBar.SetActive(true);
        stats.maxHealth = 12000;
        stats.health = 12000;
        stats.movementSpeed = 3.55f;
        currentPhase++;
        allowMoves = true;
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="GameBarrier"){
            ScreenShake.Instance.ShakeCamera(rb.gravityScale,0.5f);
            if(rb.gravityScale>0){
             rb.gravityScale--;  
             rb.velocity = Vector2.up *rb.gravityScale*3.0f; 
            }
            if(rb.gravityScale==0&&!bossAwake){
            instantiatedHealthBar = Instantiate(NathanYuHealthBar,new Vector2(960f,960f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            Instantiate(NathanYuSpeech,new Vector2(960f,120f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
            bossAwake = true;
            stats.invincible = false;
            }
        }
    }
    IEnumerator collisionDamage(Collision2D other){
        allowCollisionDamage = false;
    other.gameObject.GetComponent<Stats>().decreaseHealth(30);   
    yield return new WaitForSeconds(0.5f);
    allowCollisionDamage = true;
    }
    private void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"){
            if(allowCollisionDamage){
             StartCoroutine(collisionDamage(other));
            }
        }

    }
}
