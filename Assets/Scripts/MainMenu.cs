using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] SceneFadeController sceneFadeController;

    void Start()
    {
        startButton.onClick.AddListener(startPressed);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            
        }
    }

    void startPressed(){
        StartCoroutine(sceneFadeController.fadeOutAndLoadScene("Island"));
    }

    
}
