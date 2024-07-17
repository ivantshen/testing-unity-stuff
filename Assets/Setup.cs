using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Setup : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject abilityBar;
    [SerializeField] private GameObject character = null;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject EnvironmentCanvas;
    private Transform Env;
    // Start is called before the first frame update
    void Start()
    {
        Env = gameObject.transform;
        Instantiate(abilityBar,new Vector2(Screen.width*0.07f,Screen.height*0.9f),Quaternion.identity,EnvironmentCanvas.transform);
        Instantiate(character, new Vector3(0f,0f,0f),Quaternion.identity,Env);
        Instantiate(healthBar,new Vector2(Screen.width*0.07f,Screen.height*0.95f),Quaternion.identity,EnvironmentCanvas.transform);
        Instantiate(boss,new Vector2(18.5f,9.5f),Quaternion.Euler(180f,0f,180f),Env);
    }
}
