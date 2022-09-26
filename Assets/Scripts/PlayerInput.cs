using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerAttrib playerAttrib;
    [SerializeField] PlayerActions playerActions;
    [SerializeField] Button moveButton, rocketButton, shellButton, endTurnButton;
    public enum Mode{
        Nothing,
        Move,
        Rocket,
        Shell
    }
    public Mode currentMode;
    public bool isWaiting;

    void Start(){
        moveButton.onClick.AddListener(movePressed);
        shellButton.onClick.AddListener(shellPressed);
        rocketButton.onClick.AddListener(rocketPressed);
    }

    private void movePressed(){
        if(playerAttrib.IsPlayerTurn() && !isWaiting){
            if(currentMode == Mode.Move){
                playerActions.cleanExitMoveMode();
                currentMode = Mode.Nothing;
                Debug.Log("movement mode off");
            } else {
                currentMode = Mode.Move;
                Debug.Log("movement mode on");
            }

        }
    }
    private void shellPressed(){
        if(playerAttrib.IsPlayerTurn() && !isWaiting){
            if(currentMode == Mode.Shell){
                currentMode = Mode.Nothing;
                
                Debug.Log("shell mode off");
            } else {
                playerActions.cleanExitMoveMode();
                currentMode = Mode.Shell;
                Debug.Log("shell mode on");
            }

        }
    }
    private void rocketPressed(){
        if(playerAttrib.IsPlayerTurn() && !isWaiting){
            if(currentMode == Mode.Rocket){
                currentMode = Mode.Nothing;
                Debug.Log("rocket mode off");
            } else {
                playerActions.cleanExitMoveMode();
                currentMode = Mode.Rocket;
                Debug.Log("rocket mode on");
            }

        }
    }
    void Update()
    {
        if(playerAttrib.IsPlayerTurn()){
            if(!isWaiting){
                if(Input.GetMouseButtonDown(0)){
                    RaycastHit result;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    bool uiBlock = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                    if(Physics.Raycast(ray, out result, 100.0f) && !uiBlock){
                        if(currentMode == Mode.Move){
                            playerActions.setDestination(result);
                        } else if(currentMode == Mode.Rocket){
                            playerActions.shootRocket(result);
                        } else if(currentMode == Mode.Shell){
                            playerActions.shootShell(result);
                        }
                    }
                }

                if(Input.GetMouseButtonDown(1)){
                    isWaiting = true;
                    playerActions.goDestination();
                }

            } else if(currentMode == Mode.Move){
                if(playerActions.isMoving() == false){
                    isWaiting = false;
                }
            } else{
                Debug.LogError("Wait state error, wait cancelled");
                isWaiting = false;
            }
        }
    }
}
