using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions;
    [SerializeField] Toggle moveButton, rocketButton, shellButton;
    PlayerAttrib playerAttrib;
    [SerializeField] float camSpeed = 50;
    bool moveEnable;
    public enum Mode{
        Nothing,
        Move,
        Rocket,
        Shell
    }
    public Mode currentMode;
    public bool isWaiting;

    void Start(){
        playerAttrib = playerActions.playerAttrib;
        moveButton.onValueChanged.AddListener(movePressed);
        shellButton.onValueChanged.AddListener(shellPressed);
        rocketButton.onValueChanged.AddListener(rocketPressed);
    }

    private void movePressed(bool active){
            if(playerAttrib.IsPlayerTurn() && !isWaiting){
                if(currentMode == Mode.Move && !active){
                    playerActions.cleanExitMoveMode();
                    currentMode = Mode.Nothing;
                    //Debug.Log("movement mode off");
                } else {
                    currentMode = Mode.Move;
                    //Debug.Log("movement mode on");
                }
            }
    }
    private void shellPressed(bool active){
            if(playerAttrib.IsPlayerTurn() && !isWaiting){
                if(currentMode == Mode.Shell && !active){
                    currentMode = Mode.Nothing;
                    
                    //Debug.Log("shell mode off");
                } else {
                    playerActions.cleanExitMoveMode();
                    currentMode = Mode.Shell;
                    //Debug.Log("shell mode on");
                }
        }
    }
    private void rocketPressed(bool active){
            if(playerAttrib.IsPlayerTurn() && !isWaiting){
                if(currentMode == Mode.Rocket && !active){
                    currentMode = Mode.Nothing;
                    //Debug.Log("rocket mode off");
                } else {
                    playerActions.cleanExitMoveMode();
                    currentMode = Mode.Rocket;
                    //ebug.Log("rocket mode on");
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

            float rotation = Input.GetAxis("Horizontal") * camSpeed;
            playerActions.rotateCam(rotation);
        } else if (!(currentMode == 0)){
            currentMode = 0;
        }
    }
}
