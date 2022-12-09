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
    public GameObject boss;
    private GameObject canvasBackground;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(AddStuff);
        characterSelectButtons[0].onClick.AddListener(()=>SetCharacter(characterList[0]));
        characterSelectButtons[1].onClick.AddListener(()=>SetCharacter(characterList[1]));
        characterSelectButtons[2].onClick.AddListener(()=>SetCharacter(characterList[2]));
        characterSelectButtons[3].onClick.AddListener(()=>SetCharacter(characterList[3]));
        canvasBackground = GameObject.FindWithTag("BackgroundCanvas");
    }

    void AddStuff(){
        if(character!=null){
        Instantiate(abilityBar,new Vector2(Screen.width*0.07f,Screen.height*0.9f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        Instantiate(character, new Vector3(0f,0f,0f),Quaternion.identity,null);
        Instantiate(healthBar,new Vector2(Screen.width*0.07f,Screen.height*0.95f),Quaternion.identity,GameObject.FindWithTag("MainCanvas").transform);
        Instantiate(boss,new Vector2(18.5f,9.5f),Quaternion.Euler(180f,0f,180f),null);
        Destroy(canvasBackground);
        GUI.SetActive(false);
        }
         }
    void SetCharacter(GameObject character){
        this.character = character;
    }
}
