using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MLNathanYu : MonoBehaviour
{ 
    public GameObject warning;
    public Sprite enragedPhase0;
    // 0 = spin emitter 1 and 2 = split up map 3 = spitball 4 = explosion
    public GameObject[] moves;
    public Sprite[] ratStickSprites;
    public Stats stats;
    public Rigidbody2D rb;
    private int currentSizeIncrease =0;
    private int ratStacks = 0;
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
    private int extraDegTracking = 0;
    private Transform firepoint1;
    private IEnumerator currentCoroutine;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCanvas;
    private GameObject[] gameBarriers;
    private GameObject currentRatProjectile;
    private GameObject ratStick;
    private GameObject[] throwQueue = new GameObject[5];
    private int scannedObjects = 0;
    private float ratStickSpeed = 45f;
    private bool allowThrowing = true;
    private bool staggered = false;
    [SerializeField] private Transform[] walls;
    [SerializeField] private Transform enemyBullets;
    [SerializeField] private GameObject env;
    private int laserState = 0;
    private float laserTimer = 0f;
    public void setUp(Transform[]ws,GameObject p, GameObject canvas,Transform eb, GameObject e){
        walls = ws;
        player=p;
        mainCanvas=canvas;
        mainCam = Camera.main;
        enemyBullets = eb;
        env = e;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameBarriers = GameObject.FindGameObjectsWithTag("GameBarrier");
        foreach (GameObject currentBarrier in gameBarriers)
        {
            if(currentBarrier.layer==9){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),currentBarrier.GetComponent<Collider2D>());    
            }
        }
        ratStick = transform.GetChild(1).GetChild(1).gameObject;
        firepoint1 = transform.GetChild(0).transform;
        rb.gravityScale = 8f;
        stats.invincible = true;
        currentRatProjectile=moves[6];
    }

    // Update is called once per frame
    void Update()
    {
        laserTimer+=Time.deltaTime;
        if(laserTimer>=13f){
            laserState=0;
        }
        if(bossAwake){
            if(currentPhase==0&&allowMoves){
                currentCoroutine = phase0Transition();
                StartCoroutine(currentCoroutine);
            }
            /*if(currentPhase==0&&stats.health<=9500&&!enraged){
                enraged = true;
                phase1StaggerLength -=1f;
                phase1MoveChainSpeed-=1.5f;
                spitCD -=0.125f;
                StopCoroutine(currentCoroutine);
                StartCoroutine(phase0EnragedTransition());
            }*/
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
           // if(stats.health<stats.maxHealth/2&currentPhase==1){
           //     currentPhase++;
          //      ratStick.GetComponent<SpriteRenderer>().sprite = ratStickSprites[0];
          //  }
            if(allowThrowing&&scannedObjects>0){
                bool found = false;
                int indexOfThrow = -1;
                GameObject target = null;
                for(int i=0;i<throwQueue.Length;i++){
                    if(!found){
                        if(throwQueue[i]!=null){
                            target = throwQueue[i];
                            found = true;
                            indexOfThrow = i;
                        }
                    }
                }
                if(found&&target){
                 StartCoroutine(ThrowAtScannedTarget(target));
                throwQueue[indexOfThrow] = null;
                }else{
                    scannedObjects--;
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
        GameObject newSpitBall =Instantiate(moves[3],firepoint1.position,(firepoint1.rotation*offset*Quaternion.AngleAxis(-extraDegTracking,Vector3.forward)),enemyBullets);
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
          transform.rotation = Quaternion.Euler(0f,0f,rotationZ+extraDegTracking);  
        if(rotationZ <-90||rotationZ>90){
            if(player.transform.eulerAngles.y==0){
                transform.localRotation = Quaternion.Euler(180,0,-rotationZ+extraDegTracking);
            }else if(player.transform.eulerAngles.y==180){
                transform.localRotation = Quaternion.Euler(180,180,-rotationZ+extraDegTracking);
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
            Instantiate(warning,new Vector2(960f,540f),Quaternion.identity,mainCanvas.transform);
            yield return new WaitForSeconds(0.85f);
            stopSpitting = false;
            transform.localPosition = new Vector2(0,0);
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
            Instantiate(warning,new Vector2(960f,540f),Quaternion.identity,mainCanvas.transform);
            yield return new WaitForSeconds(0.85f);
            stopSpitting = false;
            transform.localPosition = new Vector2(0,0);
            
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
        staggered = true;
        stats.changeDamageTakenMultiplier(0.5f);
        //Out of bounds check
        if(transform.position.y<walls[2].position.y+4f){
        transform.position = new Vector3(transform.position.x,walls[2].position.y+4f,0f);
        }else if(transform.position.y>walls[0].position.y-4f){
        transform.position = new Vector3(transform.position.x,walls[0].position.y-4f,0f);
        }
        if(transform.position.x>walls[1].position.x-10f){
        transform.position = new Vector3(walls[1].position.x-10f,transform.position.y,0f);
        }else if(transform.position.x<walls[3].position.x+10f){
          transform.position = new Vector3(walls[3].position.x+10f,transform.position.y,0f);  
        }
        rb.gravityScale+=3;   
        stopTracking = true;
        rb.rotation=Random.Range(-75.0f,75.0f);

        }else{
        rb.rotation = 0f;
        rb.gravityScale=0;
        stats.changeDamageTakenMultiplier(-0.5f);
        staggered = false;
        stopTracking = false;
        }
    }
    private void phase1DivideMap(){
        int decision = Random.Range(1,3);
        GameObject divider = moves[decision];
        laserTimer =0f;
        if(decision==2){
        laserState = 1;
        GameObject temp = Instantiate(divider,new Vector2(0f,0f),Quaternion.identity,env.transform);
        temp.transform.localPosition = new Vector2(0,0);
        temp.transform.SetParent(enemyBullets);
        }else{
        laserState = 2;
        GameObject temp = Instantiate(divider,new Vector2(0f,0f),Quaternion.Euler(0,0,45),env.transform);
        temp.transform.localPosition = new Vector2(0,0);
        temp.transform.SetParent(enemyBullets);
        }
        
    }
    private void phase1SpinnerPlacement(){
        int amountOfPlacements = 6;
        GameObject emitter = moves[0];
        if(player!=null){
        Instantiate(emitter,player.transform.position,Quaternion.identity,mainCanvas.transform).GetComponent<MLScreenToBullet>().setEnemyBullets(enemyBullets);
        
        amountOfPlacements--;    
        }
        while(amountOfPlacements>0){
            amountOfPlacements--;
            Instantiate(emitter,new Vector2(Random.Range(walls[3].position.x+7,walls[1].position.x-7),Random.Range(walls[2].position.y+4f,walls[0].position.y-4f)),Quaternion.identity,mainCanvas.transform).GetComponent<MLScreenToBullet>().setEnemyBullets(enemyBullets);
        }
    }
    private void phase1ExplosionPlacement(){
        int amountOfPlacements = 7;
        GameObject explosion = moves[4];
        if(player!=null){
        Instantiate(explosion,player.transform.position,Quaternion.identity,mainCanvas.transform).GetComponent<MLScreenToBullet>().setEnemyBullets(enemyBullets);
        amountOfPlacements--;    
        }
        GameObject[] allSentries = GameObject.FindGameObjectsWithTag("Sentry");
        foreach (GameObject currentSentry in allSentries)
        {
            if(amountOfPlacements>0){
            Instantiate(explosion,new Vector2(Random.Range(walls[3].position.x+7,walls[1].position.x-7),Random.Range(walls[2].position.y+4f,walls[0].position.y-4f)),Quaternion.identity,mainCanvas.transform).GetComponent<MLScreenToBullet>().setEnemyBullets(enemyBullets);
            Instantiate(explosion,new Vector2(Random.Range(walls[3].position.x+7,walls[1].position.x-7),Random.Range(walls[2].position.y+4f,walls[0].position.y-4f)),Quaternion.identity,mainCanvas.transform).GetComponent<MLScreenToBullet>().setEnemyBullets(enemyBullets);
            amountOfPlacements-=2;
            }
        }
        while(amountOfPlacements>0){
            amountOfPlacements--;
             Instantiate(explosion,new Vector2(Random.Range(walls[3].position.x+7,walls[1].position.x-7),Random.Range(walls[2].position.y+4f,walls[0].position.y-4f)),Quaternion.identity,mainCanvas.transform).GetComponent<MLScreenToBullet>().setEnemyBullets(enemyBullets);
        }
    }
    private void phase2ScanHit(){
        GameObject scanner = moves[5];
        Instantiate(scanner,new Vector2(0f,0f),Quaternion.identity);
    }
    public void addToThrowQueue(GameObject target){
        bool added = false;
        for(int i=0;i<throwQueue.Length;i++){
            if(!added){
                if(throwQueue[i]==null){
                    added = true;
                    throwQueue[i] = target;
                    scannedObjects++;
                }
            }
        }
        }
    IEnumerator ThrowAtScannedTarget(GameObject target){
        allowThrowing=false;
        scannedObjects--;
         Animation anim = transform.GetChild(1).GetChild(1).gameObject.GetComponent<Animation>();
        anim.Play("throw",PlayMode.StopAll);
        yield return new WaitForSeconds(0.95f);
        anim.Play("idleRatStick",PlayMode.StopAll);
        Vector3 throwPosition = mainCam.WorldToScreenPoint(target.transform.localPosition);
        Vector3 currentPosition = mainCam.WorldToScreenPoint(transform.GetChild(1).GetChild(0).position);
        Vector2 offset = new Vector2(throwPosition.x-currentPosition.x,throwPosition.y-currentPosition.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        GameObject stick = Instantiate(currentRatProjectile,transform.GetChild(1).GetChild(0).position,Quaternion.Euler(0f,0f,angle+270));
        Physics2D.IgnoreCollision(stick.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        stick.SendMessage("assignDamage",50);
        stick.SendMessage("assignSpeed",ratStickSpeed);   
        allowThrowing=true;
    }
    public void IncreaseRatStack(){
        ratStacks++;
        if(ratStacks<=3){
            if(ratStacks==1){
            ratStick.transform.localPosition= new Vector2(0,ratStick.transform.localPosition.y);
            ratStick.GetComponent<SpriteRenderer>().sprite = ratStickSprites[1];
            currentRatProjectile = moves[7];
            ratStickSpeed=25f;
            }else if(ratStacks==2){
            ratStick.transform.localPosition= new Vector2(ratStick.transform.localPosition.x,0.23f*((currentSizeIncrease+1)*0.5f));
            ratStick.GetComponent<SpriteRenderer>().sprite = ratStickSprites[2];
            currentRatProjectile = moves[8];
            ratStickSpeed-=20f;
            }else{
            ratStick.transform.localPosition= new Vector2(-0.15f*((currentSizeIncrease+1)*0.5f),0.5f*((currentSizeIncrease+1)*0.5f));
            ratStick.GetComponent<SpriteRenderer>().sprite = ratStickSprites[3];
            currentRatProjectile = moves[9];
            ratStickSpeed=35f;
            }
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
          yield return new WaitForSeconds(1.25f);
        }
        yield return new WaitForSeconds(2.25f);
        currentPhase++;
        allowMoves = true;
    }
    IEnumerator phase0EnragedTransition(){
        allowMoves= false;
        rb.velocity = new Vector2(0f,0f);
        GetComponent<SpriteRenderer>().sprite = enragedPhase0;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        foreach (GameObject currentBarrier in gameBarriers)
        {
            if(currentBarrier.layer==9){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),currentBarrier.GetComponent<Collider2D>());    
            }
        }
        for(float i=1;i<5;i++){
            GetComponent<SpriteRenderer>().color = new Color(1f,1f-(0.15f*i),1f-(0.15f*i),1f);
            yield return new WaitForSeconds(0.35f);
        }
        for(int i=currentSizeIncrease+1;i<7;i++){
          transform.localScale += new Vector3(0.5f,0.5f,0f);  
          yield return new WaitForSeconds(1.25f);
        }
        yield return new WaitForSeconds(3.25f);
        stats.maxHealth = 12000;
        stats.health = 12000;
        stats.movementSpeed = 3.55f;
        extraDegTracking =25;
        currentPhase++;
        allowMoves = true;
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="GameBarrier"){
            if(rb.gravityScale>0){
             rb.gravityScale--;  
             rb.velocity = Vector2.up *rb.gravityScale*3.0f; 
            }
            if(rb.gravityScale==0&&!bossAwake){
            bossAwake = true;
            stats.invincible = false;
            player.GetComponent<MLStats>().invincible = false;
            }
        }
    }
    IEnumerator collisionDamage(Collision2D other,int dmg){
        allowCollisionDamage = false;
    if(other.gameObject.GetComponent<Stats>()){
    other.gameObject.GetComponent<Stats>().decreaseHealth(dmg);     
    }else if (other.gameObject.GetComponent<MLStats>()){
    other.gameObject.GetComponent<MLStats>().decreaseHealth(dmg);     
    }
      
    yield return new WaitForSeconds(0.5f);
    allowCollisionDamage = true;
    }
    private void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag=="Player"||other.gameObject.tag=="Sentry"){
            if(allowCollisionDamage){
             StartCoroutine(collisionDamage(other,30));
            }
        }else if(other.gameObject.tag=="PlayerBarricade"){
            if(allowCollisionDamage){
             StartCoroutine(collisionDamage(other,65));
            }
        }

    }
    public bool getStaggerState(){
        return staggered;
    }
    public bool getBossChaseState(){
        return bossChasing;
    }
    public bool getBossSpitState(){
        return bossSpitting;
    }
    public int getLaserState(){
        return laserState;
    }
}
