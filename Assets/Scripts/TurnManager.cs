using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private PlayerAttrib playerOne, playerTwo;
    [SerializeField] private int numberOfPlayers = 2;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SceneFadeController sceneFadeController;
    [SerializeField] private float timeBetweenTurns;
    [SerializeField] private Transform playerOneCam, playerTwoCam;
    [SerializeField] public Hud hud;
    
    public int currentPlayerIndex, nextPlayerIndex, winner;
    public bool waitingForNextTurn, gameOverTurn, stop;
    private float turnDelay;

    private void Awake(){
        if (instance == null){
            instance = this;
            currentPlayerIndex = 1;
            nextPlayerIndex = 2;
            playerOne.SetPlayerTurn(1);
            playerTwo.SetPlayerTurn(2);
            cameraController.SetCamera(playerOneCam);
        }
    }

    private void Update(){
        if (waitingForNextTurn && !gameOverTurn){
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeBetweenTurns){
                turnDelay = 0;
                waitingForNextTurn = false;
                ChangeTurn();
            }
        }
        if (gameOverTurn && !stop){
            stop = true;
            winner = GetWinner();
            PlayerPrefs.SetInt("winner", winner);
            TriggerChangeTurn();
            StartCoroutine(sceneFadeController.FadeOutAndLoadScene("GameOver"));
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
        hud.NoHud();
        cameraController.SetIdle();
        waitingForNextTurn = true;
    }

    private void ChangeTurn(){
        if (currentPlayerIndex == 1){
            currentPlayerIndex = 2;
            cameraController.SetCamera(playerTwoCam);
        }
        else if (currentPlayerIndex == 2){
            currentPlayerIndex = 1;
            cameraController.SetCamera(playerOneCam);
        }
    }

    public void PlayerKill(){
        numberOfPlayers--;
        if (numberOfPlayers <= 1){
            gameOverTurn = true;
        }
    }

    private int GetWinner(){
        if (!playerOne.dead){
            return 1;
        } else if (!playerTwo.dead){
            return 2;
        } else {
            return 0;
        }
    }
}
