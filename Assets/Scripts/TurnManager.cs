using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PlayerAttrib playerOne, playerTwo;
    [SerializeField] private int numberOfPlayers = 2;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SceneFadeController sceneFadeController;
    [SerializeField] private float timeBetweenTurns;
    [SerializeField] private Transform playerOneCam, playerTwoCam;
    [SerializeField] public Hud hud;
    
    public int currentPlayerIndex, nextPlayerIndex, winner, topscore;
    public bool waitingForNextTurn, gameOverTurn, stop;
    private float turnDelay;

    private void Awake(){
        currentPlayerIndex = 1;
        nextPlayerIndex = 2;
        playerOne.SetPlayerTurn(1);
        playerTwo.SetPlayerTurn(2);
        cameraController.SetCamera(playerOneCam);
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
            topscore = GetScore(winner);
            PlayerPrefs.SetInt("currentscore", topscore);
            if (topscore < PlayerPrefs.GetInt("topscore", 0)){
                topscore = PlayerPrefs.GetInt("topscore", 0);
            }
            PlayerPrefs.SetInt("winner", winner);
            PlayerPrefs.SetInt("topscore", topscore);
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

    public void Score(int damage, int index){
        int points = 10;
        if (damage == 30){
            points = points + 500;
        } else if (damage > 11){
            points = points + 250;
        } else if (damage > 8){
            points = points + 100;
        } 

        if (index == 1){
            playerTwo.AddScore(points);
        } else if (index == 2){
            playerOne.AddScore(points);
        }
    }

    public int GetScore(int index){
        if (index == 1){
            return playerOne.score;
        } else if (index == 2){
            return playerTwo.score;
        }


        return 0;
    }
}
