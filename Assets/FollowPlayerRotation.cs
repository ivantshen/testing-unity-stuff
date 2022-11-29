using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerRotation : MonoBehaviour
{
    public bool trackPlayer = true;
    public bool trackBarricade = false;
    public bool trackSentry = false;
    private bool allowTracking = true;
    private GameObject player;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowTracking){
        StartCoroutine(waitToTrack());
        }  
    }

    IEnumerator waitToTrack(){
        allowTracking=false;
        Vector3 position = transform.position;

        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] allSentries = GameObject.FindGameObjectsWithTag("Sentry");
        GameObject[] allBarricades = GameObject.FindGameObjectsWithTag("PlayerBarricade");
        float distance = Mathf.Infinity;

        if(allPlayers.Length!=0||allSentries.Length!=0||allBarricades.Length!=0){
        if(trackBarricade){
        foreach (GameObject currentBarricade in allBarricades){
            Vector3 diff = currentBarricade.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance<distance){
                player = currentBarricade;
                distance = currentDistance;
            }
        }    
        }
        if(trackPlayer){
        foreach (GameObject currentPlayer in allPlayers){
            Vector3 diff = currentPlayer.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance<distance){
                player = currentPlayer;
                distance = currentDistance;
            }
        }    
        }
        if(trackSentry){
        foreach (GameObject currentSentry in allSentries){
            Vector3 diff = currentSentry.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance<distance){
                player = currentSentry;
                distance = currentDistance;
            }
        }    
        }
        Vector3 playerPosition = mainCamera.WorldToScreenPoint(player.transform.localPosition);
        Vector3 currentPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(playerPosition.x-currentPosition.x,playerPosition.y-currentPosition.y);
        float angle = Mathf.Atan2(offset.y,offset.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
        yield return new WaitForSeconds(0.05f);
        allowTracking=true;
        }
    }
}
