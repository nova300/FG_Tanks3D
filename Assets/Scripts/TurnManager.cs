using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private PlayerTurn playerOne;
    [SerializeField] private PlayerTurn playerTwo;
    [SerializeField] private float timeBetweenTurns;
    
    public int currentPlayerIndex, nextPlayerIndex;
    public bool waitingForNextTurn;
    private float turnDelay;

    private void Awake(){
        if (instance == null){
            instance = this;
            currentPlayerIndex = 1;
            nextPlayerIndex = 2;
            playerOne.SetPlayerTurn(1);
            playerTwo.SetPlayerTurn(2);
            CameraManager.GetInstance().changeCamera(1);
        }
    }

    private void Update(){
        if (waitingForNextTurn){
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeBetweenTurns){
                turnDelay = 0;
                waitingForNextTurn = false;
                ChangeTurn();
            }
        }
    }

    public bool IsItPlayerTurn(int index){
        if (waitingForNextTurn){ 
            return false;
        }
        return index == currentPlayerIndex;
    }

    public static TurnManager GetInstance(){
        return instance;
    }

    public void TriggerChangeTurn(){
        CameraManager.GetInstance().changeCamera(0);
        waitingForNextTurn = true;
    }

    private void ChangeTurn(){
        if (currentPlayerIndex == 1){
            currentPlayerIndex = 2;
            CameraManager.GetInstance().changeCamera(2);
        }
        else if (currentPlayerIndex == 2){
            currentPlayerIndex = 1;
            CameraManager.GetInstance().changeCamera(1);
        }
    }
}
