using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartFight : MonoBehaviour
{
    public GameObject GUI;
    public GameObject[] characterList;
    public GameObject healthBar;
    public GameObject abilityBar;
    private GameObject character = null;
    public Button[] characterSelectButtons;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(AddStuff);
        characterSelectButtons[0].onClick.AddListener(SetCharacter(characterList[0]));
    }

    void AddStuff(){
        if(character!=null){
        Instantiate(character, new Vector3(0f,0f,0f),Quaternion.identity,null);
        Instantiate(healthBar,new Vector2(Screen.width*0.05f,Screen.height*0.95f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        Instantiate(abilityBar,new Vector2(Screen.width*0.05f,Screen.height*0.9f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        }else{
            GUI.SetActive(false);
        }
         }
    void SetCharacter(GameObject character){

    }
}
