using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp, ap, score;
    [SerializeField] ToggleGroup toggleGroup;
    int hpVal,apVal,moveCost,scoreVal;
    bool hudOn;

    void Update(){
        if(!hudOn && toggleGroup.AnyTogglesOn()){
            toggleGroup.SetAllTogglesOff();
        }
    }

    public void SetHud(int n_hpVal, int n_apVal, int n_score, bool force = false){
        hudOn = true;
        if(n_hpVal != hpVal || n_apVal != apVal || n_score != scoreVal || force){
            hpVal = n_hpVal;
            apVal = n_apVal;
            scoreVal = n_score;
            if(moveCost >= 1){
                ap.SetText("AP: " + apVal + " (-" + moveCost + ")");
            } else {
                ap.SetText("AP: " + apVal);
            }
            hp.SetText("HP: " + hpVal);
            score.SetText("" + scoreVal);
        }
    }

    public void NoHud(){
        hudOn = false;
        ap.SetText("");
        hp.SetText("");
        score.SetText("");
        toggleGroup.SetAllTogglesOff();
    }

    public void SetMoveCost(int newMoveCost){
        if(moveCost != newMoveCost){
            moveCost = newMoveCost;
            SetHud(hpVal, apVal, scoreVal, true);
        }
    }
}