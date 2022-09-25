using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttrib : MonoBehaviour
{
    [SerializeField] public int hp=100,ap=10,apRefill=10,apMax=10,mode;
    public bool wait;

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
        Debug.Log("player damaged for " + amount + " points");
    }

    public int getAP(){
        return ap;
    }

    public bool apIsMoveAllowed(int cost){
        if(ap >= cost){
            return true;
        } else {
            return false;
        }
    }

    public void deductAP(int amount){
        ap = ap - amount;
    }

    public int getMode(){
        return mode;
    }

    public void setMode(int n_mode){
        mode = n_mode;
    }

    public void setWait(bool nWait){
        wait = nWait;
        Debug.Log("wait = " + nWait);
    }

    public bool isWaiting(){
        return wait;
    }

}
