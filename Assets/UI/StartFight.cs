using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public TMP_Text selectedCharacterText;
    private GameObject canvasBackground;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(AddStuff);
        characterSelectButtons[0].onClick.AddListener(()=>SetCharacter(characterList[0]));
        characterSelectButtons[1].onClick.AddListener(()=>SetCharacter(characterList[1]));
        characterSelectButtons[2].onClick.AddListener(()=>SetCharacter(characterList[2]));
        characterSelectButtons[3].onClick.AddListener(()=>SetCharacter(characterList[3]));
        characterSelectButtons[4].onClick.AddListener(()=>SetCharacter(characterList[4]));
        canvasBackground = GameObject.FindWithTag("BackgroundCanvas");
    }

    void AddStuff(){
        if(character!=null){
        Instantiate(character, new Vector3(0f,0f,0f),Quaternion.identity,null);
        Instantiate(boss,new Vector2(18.5f,9.5f),Quaternion.Euler(180f,0f,180f),null);
        healthBar.GetComponent<UpdatePlayerHp>().delayedStart();
        
        canvasBackground.GetComponent<Image>().enabled = false;
        GUI.SetActive(false);
        }
         }
    void SetCharacter(GameObject character){
        this.character = character;
        selectedCharacterText.text = "Current Character: " + character.name;
    }
}
