using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton,settingsButton;
    [SerializeField] AudioMixer mixer;
    [SerializeField] SceneFadeController sceneFadeController;

    void Start(){
        mixer.SetFloat("masterVol", Mathf.Log10(PlayerPrefs.GetFloat("mainVol", 0.75f)) * 20);
        startButton.onClick.AddListener(StartPressed);
        settingsButton.onClick.AddListener(SettingsPressed);
    }

    void StartPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("Island"));
    }

    void SettingsPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("SettingsMenu"));
    }

    
}
