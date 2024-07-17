using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class MLStats : Agent
{
    public int maxHealth;
    public int health;
    public float movementSpeed;
    public bool invincible = false;
    private float damageTakenMultiplier =1;
    public bool isPlayer = false;
    [SerializeField] private GameObject boss;
    public Rigidbody2D rb;
    public bool allowMovement = true;
    private LayerMask targetLayer;
    [SerializeField] private Transform[] walls;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private MLWeaponRotation wepRot;
    [SerializeField] private MLWeaponShooting shoot;
    [SerializeField] private MLHunterAbility ability;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private MLBossHealthBar hpBar;
    [SerializeField] private Transform env;
    [SerializeField] private GameObject playerBullets;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform enemyBullets;
    private MLNathanYu b;
    [SerializeField] private bool timeConstraint = false;
    [SerializeField] private float testTime = 30f;
    [SerializeField] private float timeReward = -500f;
    [SerializeField] private float timeRewardIncrement = 0.1f;
    [SerializeField] private int stage;
    private float timeConstraintTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Physics2D.IgnoreLayerCollision(3,3);
        Physics2D.IgnoreLayerCollision(3,7);
        targetLayer = LayerMask.GetMask("EnemyBullets");
        for(int i=0;i<canvas.transform.childCount;i++){
            Debug.Log((Vector2)canvas.transform.GetChild(i).localPosition);
        }
    }
    void FixedUpdate(){
        if(timeConstraint){
            timeConstraintTimer+=Time.deltaTime;
            if(timeConstraintTimer>=testTime){
                AddReward(timeReward);
                EndEpisode();
            }
        }
        AddReward(timeRewardIncrement*Time.deltaTime);
    }
    public override void OnEpisodeBegin(){
        invincible = true;
        timeConstraintTimer=0f;
        foreach(Transform child in canvas.transform){
            if(child.name!="HealthBar"&&child.name!="AbilityBar"&&child.name!="NathanYuBossHealth"){
            Destroy(child.gameObject);    
            }
        }
        foreach(Transform child in playerBullets.transform){
            Destroy(child.gameObject);
        }
        foreach(Transform child in enemyBullets){
            Destroy(child.gameObject);
        }
        Destroy(boss);
        gameObject.transform.position = new Vector2(Random.Range(walls[3].position.x+8f,walls[1].position.x-10f),Random.Range(walls[2].position.y+6f,walls[0].position.y-6f));
        boss = Instantiate(bossPrefab,new Vector2(Random.Range(walls[3].position.x+8f,walls[1].position.x-10f),Random.Range(walls[2].position.y+6f,walls[0].position.y-6f)),Quaternion.identity,env);
        boss.transform.rotation = Quaternion.Euler(180f,0,180f);
        boss.GetComponent<Stats>().setAgent(this);
        b = boss.GetComponent<MLNathanYu>();
        if(b){
        b.setUp(walls,this.gameObject,canvas,enemyBullets,env.gameObject);    
        }
        if(stage==2){
            boss.GetComponent<Stage2MLTarget>().setUp(this.gameObject,enemyBullets);
        }
        hpBar.setStats(boss.GetComponent<Stats>());
        ability.reset();
        health = maxHealth;
    }
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation((Vector2)boss.transform.localPosition);
        sensor.AddObservation((Vector2)transform.localPosition);
        if(b){
        sensor.AddObservation((int)b.getLaserState());
        sensor.AddObservation((bool)b.getBossChaseState());
        sensor.AddObservation((bool)b.getBossSpitState());
        sensor.AddObservation((bool)b.getStaggerState());    
        }
        foreach(Transform child in canvas.transform){
            if(child.name!="HealthBar"&&child.name!="AbilityBar"&&child.name!="NathanYuBossHealth"){
            BulletType b = child.gameObject.GetComponent<BulletType>();
            sensor.AddObservation(new Vector3(child.localPosition.x,child.localPosition.y,b.getBulletType()));    
            }
        }
        foreach (Transform child in enemyBullets)
        {
            BulletType b = child.gameObject.GetComponent<BulletType>();
            sensor.AddObservation(new Vector3(child.localPosition.x,child.localPosition.y,b.getBulletType()));
        }
    }
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        Vector2 difference = new Vector2(actions.ContinuousActions[2],actions.ContinuousActions[3]).normalized;
        float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
        if(allowMovement){
        rb.velocity = new Vector2(moveX,moveY)*movementSpeed;      
        }
        wepRot.setAngle(rotationZ);;    
        if(actions.DiscreteActions[0]==1){
            ability.tryFire();
        }
        if(actions.DiscreteActions[1]==1){
            shoot.generateBullet();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ability.tryFire();
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    public void changeDamageTakenMultiplier(float mult){
        damageTakenMultiplier+=mult;
    }
    public void decreaseHealth(int amtToDecrease){
        if(!invincible){
         health-=(int)(amtToDecrease*damageTakenMultiplier);
         AddReward(-(int)(amtToDecrease*damageTakenMultiplier));
        if(health<=0){
            EndEpisode();
        }   
        }
        
    }
    public void increaseHealth(int amtToIncrease){
        health+=amtToIncrease;
        if(health>maxHealth){
            health = maxHealth;
        }
    }
    public void assignHealth(int hp){
        maxHealth = hp;
        health = hp;
    }
    public void speedChangePercent(float percent, float duration){
        movementSpeed *=(1+percent);
        AddReward(-10f*percent);
        StartCoroutine(speedReturn(percent,duration));

    }
    IEnumerator speedReturn(float percent, float duration){
        yield return new WaitForSeconds(duration);
        movementSpeed /=(1+percent);
    }
    public void increaseReward(float amtToIncrease){
        AddReward(amtToIncrease);
    }
    public void end(){
        EndEpisode();
    }
    public Transform getEnemyBullets(){
        return enemyBullets;
    }
}
