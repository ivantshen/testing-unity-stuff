using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disable : MonoBehaviour
{
    public GameObject buttonGameObject;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = buttonGameObject.GetComponent<Button>();
        button.onClick.AddListener(disable);
    }
    void disable(){
        buttonGameObject.SetActive(false);
    }
}
