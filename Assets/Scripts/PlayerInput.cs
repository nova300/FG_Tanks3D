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
        moveButton.onValueChanged.AddListener(MovePressed);
        shellButton.onValueChanged.AddListener(ShellPressed);
        rocketButton.onValueChanged.AddListener(RocketPressed);
    }

    private void MovePressed(bool active){
            if(playerAttrib.IsPlayerTurn() && !isWaiting){
                if(currentMode == Mode.Move && !active){
                    playerActions.CleanExitMoveMode();
                    currentMode = Mode.Nothing;
                } else {
                    currentMode = Mode.Move;
                }
            } else if (playerAttrib.IsPlayerTurn() && !active){
                moveButton.SetIsOnWithoutNotify(true);
            }
    }
    private void ShellPressed(bool active){
            if(playerAttrib.IsPlayerTurn() && !isWaiting){
                if(currentMode == Mode.Shell && !active){
                    currentMode = Mode.Nothing;
                } else {
                    playerActions.CleanExitMoveMode();
                    currentMode = Mode.Shell;
                }
        } else if (playerAttrib.IsPlayerTurn() && active){
                shellButton.isOn = false;
        }
    }
    private void RocketPressed(bool active){
            if(playerAttrib.IsPlayerTurn() && !isWaiting){
                if(currentMode == Mode.Rocket && !active){
                    currentMode = Mode.Nothing;
                } else {
                    playerActions.CleanExitMoveMode();
                    currentMode = Mode.Rocket;
                }
        } else if (playerAttrib.IsPlayerTurn() && active){
                rocketButton.isOn = false;
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
                            playerActions.SetDestination(result);
                        } else if(currentMode == Mode.Rocket){
                            playerActions.ShootRocket(result);
                        } else if(currentMode == Mode.Shell){
                            playerActions.ShootShell(result);
                        }
                    }
                }

                if(Input.GetMouseButtonDown(1) && currentMode == Mode.Move){
                    isWaiting = true;
                    playerActions.GoDestination();
                }

            } else if(currentMode == Mode.Move){
                if(playerActions.IsMoving() == false){
                    isWaiting = false;
                }
            } else{
                isWaiting = false;
            }

            float rotation = Input.GetAxis("Horizontal") * camSpeed;
            playerActions.RotateCam(rotation);
        } else if (!(currentMode == 0)){
            currentMode = 0;
        }
    }
}
