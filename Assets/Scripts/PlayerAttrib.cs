using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttrib : MonoBehaviour
{
    [SerializeField] public int hp=100,ap=10,apRefill=10,apMax=10;

    void Update(){

        /* Act point logic */
        if(ap < 1){
            TurnManager.GetInstance().TriggerChangeTurn();
            ap = ap + apRefill;
        }
        if(ap > apMax){
            ap = apMax;
        }

        /* Health point logic */
        if(hp < 1){
            //kill the player

        }

    }

    public int getHP(){
        return hp;
    }

    public void damage(int amount){
        hp = hp - amount;
    }

    public int getAP(){
        return ap;
    }

    public void deductAP(int amount){
        ap = ap - amount;
    }

}
