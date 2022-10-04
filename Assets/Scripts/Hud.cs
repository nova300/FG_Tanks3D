using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp, ap;
    [SerializeField] ToggleGroup toggleGroup;
    int hpVal,apVal,moveCost;
    bool hudOn;

    void Update(){
        if(!hudOn && toggleGroup.AnyTogglesOn()){
            toggleGroup.SetAllTogglesOff();
        }
    }

    public void SetHud(int n_hpVal, int n_apVal, bool force = false){
        hudOn = true;
        if(n_hpVal != hpVal || n_apVal != apVal || force){
            hpVal = n_hpVal;
            apVal = n_apVal;
            if(moveCost >= 1){
                ap.SetText("AP: " + apVal + " (-" + moveCost + ")");
            } else {
                ap.SetText("AP: " + apVal);
            }
            hp.SetText("HP: " + hpVal);
        }
    }

    public void NoHud(){
        hudOn = false;
        ap.SetText("");
        hp.SetText("");
        toggleGroup.SetAllTogglesOff();
    }

    public void SetMoveCost(int newMoveCost){
        if(moveCost != newMoveCost){
            moveCost = newMoveCost;
            SetHud(hpVal, apVal, true);
        }
    }
}