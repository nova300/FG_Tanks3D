using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private PlayerAttrib playerOne;
    [SerializeField] private PlayerAttrib playerTwo;
    [SerializeField] private CameraController cameraController;
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
            cameraController.setCamera(playerOne.transform, CameraController.Mode.topview);
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
        cameraController.setIdle();
        waitingForNextTurn = true;
    }

    private void ChangeTurn(){
        if (currentPlayerIndex == 1){
            currentPlayerIndex = 2;
            cameraController.setCamera(playerTwo.transform, CameraController.Mode.topviewInverted);
        }
        else if (currentPlayerIndex == 2){
            currentPlayerIndex = 1;
            cameraController.setCamera(playerOne.transform, CameraController.Mode.topview);
        }
    }
}
