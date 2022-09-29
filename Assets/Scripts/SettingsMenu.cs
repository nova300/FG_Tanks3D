using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] SceneFadeController sceneFadeController;

    void Start(){
        exitButton.onClick.AddListener(exitPressed);
    }


    void exitPressed(){
        StartCoroutine(sceneFadeController.fadeOutAndLoadScene("MainMenu"));
    }
}
