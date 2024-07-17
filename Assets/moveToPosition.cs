using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToPosition : MonoBehaviour
{
    [SerializeField] private Vector2 positionToMoveTo;
    [SerializeField] private float moveTimeDuration;
    [SerializeField] private float whenToStartMoveTime;
    [SerializeField] private Transform objectToMove;
    private float timer;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector2.Distance(positionToMoveTo,objectToMove.position);
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer<=whenToStartMoveTime){

        }else{
            float step = distance/moveTimeDuration * Time.deltaTime;
            objectToMove.position = Vector3.MoveTowards(objectToMove.position,positionToMoveTo,step);
        }
    }
    public void changeSettings(Vector2 pos,float duration,float startMoveTime,Transform whatToMove){
        positionToMoveTo = pos;
        moveTimeDuration = duration;
        whenToStartMoveTime = startMoveTime;
        objectToMove = whatToMove;
    }
}
