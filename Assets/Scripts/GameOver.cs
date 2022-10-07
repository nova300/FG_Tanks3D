using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image p1Win,p2Win,draw;
    [SerializeField] private Button restart, exit;
    [SerializeField] private SceneFadeController sceneFadeController;
    [SerializeField] private TextMeshProUGUI highScore, currentScore;
    public int winner = 1;
    public float fadeSpeed = 1.0f;
    private bool invert;
    Color objectColor;

    void OnEnable(){
        winner = PlayerPrefs.GetInt("winner");
        highScore.SetText("High Score: " + PlayerPrefs.GetInt("topscore", 0));
        currentScore.SetText("Score: " + PlayerPrefs.GetInt("currentscore", 0));
    }

    void Start(){
        restart.onClick.AddListener(RestartPressed);
        exit.onClick.AddListener(ExitPressed);
    }

    void RestartPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("Island"));
    }

    void ExitPressed(){
        StartCoroutine(sceneFadeController.FadeOutAndLoadScene("MainMenu"));
    }
    void Update(){
        float fadeAmount;
        Color objectColor = GetColor();
        if (!invert){
                if (objectColor.a < 1) {
                    fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    SetColor(objectColor);
                } else {
                    invert = true;
                }
            } else {
                if (objectColor.a > 0){
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    SetColor(objectColor);
                } else {
                    invert = false;
                }
                
            }
    }

    void SetColor(Color newColor){
        if (winner == 1){
            p1Win.color = newColor;
        } else if (winner == 2){
            p2Win.color = newColor;
        } else {
            draw.color = newColor;
        }
    }

    Color GetColor(){
        if (winner == 1){
            return p1Win.color;
        } else if (winner == 2){
            return p2Win.color;
        } else {
            return draw.color;
        }
    }

}
