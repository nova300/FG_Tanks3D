using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image p1Win,p2Win,draw;
    public int winner = 1;
    public float fadeSpeed = 1.0f;
    private bool invert;
    Color objectColor;

    void OnEnable(){
        winner = PlayerPrefs.GetInt("winner");
        Debug.Log(winner);
    }
    void Update(){
        float fadeAmount;
        Color objectColor = getColor();
        if (!invert){
                if (objectColor.a < 1) {
                    fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    setColor(objectColor);
                } else {
                    invert = true;
                }
            } else {
                if (objectColor.a > 0){
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    setColor(objectColor);
                } else {
                    invert = false;
                }
                
            }
    }

    void setColor(Color newColor){
        if (winner == 1){
            p1Win.color = newColor;
        } else if (winner == 2){
            p2Win.color = newColor;
        } else {
            draw.color = newColor;
        }
    }

    Color getColor(){
        if (winner == 1){
            return p1Win.color;
        } else if (winner == 2){
            return p2Win.color;
        } else {
            return draw.color;
        }
    }

}
