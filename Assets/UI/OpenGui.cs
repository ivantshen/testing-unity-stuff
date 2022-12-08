using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenGui : MonoBehaviour
{
    public Button button;
    public GameObject gui;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OpenCharacterGui);
    }
    void OpenCharacterGui(){
        gui.SetActive(true);
    }
}
