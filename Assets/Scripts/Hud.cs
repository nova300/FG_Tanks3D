using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp, ap;
    [SerializeField] ToggleGroup toggleGroup;
    int moveCost;
    bool hudOn;

    void Update(){
        if(!hudOn && toggleGroup.AnyTogglesOn()){
            toggleGroup.SetAllTogglesOff();
        }
    }

    public void setHud(int hpVal, int apVal){
        hudOn = true;
        if(moveCost >= 1){
            ap.SetText("AP: " + apVal + " (-" + moveCost + ")");
        } else {
            ap.SetText("AP: " + apVal);
        }
        hp.SetText("HP: " + hpVal);
    }

    public void noHud(){
        hudOn = false;
        ap.SetText("");
        hp.SetText("");
        toggleGroup.SetAllTogglesOff();
    }

    public void setMoveCost(int newMoveCost){
        moveCost = newMoveCost;
    }
}
