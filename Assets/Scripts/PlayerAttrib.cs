using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttrib : MonoBehaviour
{
    [SerializeField] public int hp=100,ap=10,apRefill=10,apMax=10,mode;
    [SerializeField] public TurnManager turnManager;
    
    public bool wait,dead;
    private int playerIndex;

    void Start(){
        
    }

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
        if(hp < 1 && dead == false){
            dead = true;
            turnManager.playerKill();
        }

        if (IsPlayerTurn()){
            turnManager.hud.setHud(hp, ap);
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

    public void SetPlayerTurn(int index){
        playerIndex = index;
    }

    public bool IsPlayerTurn(){
        return TurnManager.GetInstance().IsItPlayerTurn(playerIndex);
    }

    public bool getDead(){
        return dead;
    }


    


}
