using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton,settingsButton;
    [SerializeField] SceneFadeController sceneFadeController;

    void Start()
    {
        startButton.onClick.AddListener(startPressed);
        settingsButton.onClick.AddListener(settingsPressed);
    }

    void startPressed(){
        StartCoroutine(sceneFadeController.fadeOutAndLoadScene("Island"));
    }

    void settingsPressed(){
        StartCoroutine(sceneFadeController.fadeOutAndLoadScene("SettingsMenu"));
    }

    
}
