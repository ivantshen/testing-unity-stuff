using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RestartGame : MonoBehaviour
{
    public Button deathScreenButton;
    // Start is called before the first frame update
    void Start()
    {
        if(deathScreenButton){
        deathScreenButton.onClick.AddListener(() => resetGame());    
        }
        gameObject.SetActive(false);
    }
    void resetGame(){
        SceneManager.LoadScene("NathanBossFight");
    }
}