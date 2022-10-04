using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] SceneFadeController sceneFadeController;

    void Start(){
        exitButton.onClick.AddListener(ExitPressed);
        slider.value = PlayerPrefs.GetFloat("mainVol", 0.75f);
    }


    void ExitPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("MainMenu"));
    }

    public void SetLevel (float sliderValue){
	    audioMixer.SetFloat("masterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("mainVol", sliderValue);
    }
}
