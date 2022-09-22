using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    private static TurnManager turnManager;
    [SerializeField] CameraController cameraController;
    [SerializeField] CameraTarget cameraTarget0,cameraTarget1,cameraTarget2;
    private int currentCamId;

    private void Awake(){
        if (instance == null){
            instance = this;
            cameraController.setTarget(cameraTarget0);
        }
    }

    void Update(){

       /* if(Input.GetKeyDown(KeyCode.Space)){
            if(currentCamId < 2) {
                changeCamera(currentCamId + 1);
                currentCamId++;
            }
            else {
                currentCamId = 0;
                changeCamera(currentCamId);
            }

        } */
    }

    public void changeCamera(int id){
        if(id == 0){
            cameraController.setTarget(cameraTarget0);
            cameraController.setMode(cameraTarget0.getMode());
        } else if(id == 1){
            cameraController.setTarget(cameraTarget1);
            cameraController.setMode(cameraTarget1.getMode());
        } else if(id == 2){
            cameraController.setTarget(cameraTarget2);
            cameraController.setMode(cameraTarget2.getMode());
        } else{
            Debug.LogError("invalid camera id " + id);
        }
    }

    public static CameraManager GetInstance(){
        return instance;
    }

}
