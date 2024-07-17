using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class MLMovement : Agent
{
    [SerializeField] private Transform boss;
    public Rigidbody2D rb;
    public MLStats stats;
    public bool allowMovement = true;
    private LayerMask targetLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3,3);
        Physics2D.IgnoreLayerCollision(3,7);
        targetLayer = LayerMask.GetMask("Enemies")|LayerMask.GetMask("EnemyBullets");
    }
    public override void OnEpisodeBegin(){
        base.OnEpisodeBegin();
    }
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation((Vector2)boss.localPosition);
        sensor.AddObservation((Vector2)transform.localPosition);
        Collider2D[] enemyPositions = Physics2D.OverlapCircleAll(transform.localPosition,3f,targetLayer);
            foreach (Collider2D enemy in enemyPositions)
            {
                sensor.AddObservation((Vector2)enemy.transform.localPosition);
            }
    }
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        if(allowMovement){
        rb.velocity = new Vector2(moveX,moveY).normalized*stats.movementSpeed;      
        }
    }
    
}
