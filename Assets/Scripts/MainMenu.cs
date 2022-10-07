using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton,settingsButton;
    [SerializeField] AudioMixer mixer;
    [SerializeField] SceneFadeController sceneFadeController;
    [SerializeField] TextMeshProUGUI highScore;
 
    void Start(){
        mixer.SetFloat("masterVol", Mathf.Log10(PlayerPrefs.GetFloat("mainVol", 0.75f)) * 20);
        startButton.onClick.AddListener(StartPressed);
        settingsButton.onClick.AddListener(SettingsPressed);
        highScore.SetText("High Score: " + PlayerPrefs.GetInt("topscore", 0));
    }

    void StartPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("Island"));
    }

    void SettingsPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("SettingsMenu"));
    }

    
}
