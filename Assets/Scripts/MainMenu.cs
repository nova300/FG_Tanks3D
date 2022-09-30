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

    void Start()
    {
        mixer.SetFloat("masterVol", Mathf.Log10(PlayerPrefs.GetFloat("mainVol", 0.75f)) * 20);
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
