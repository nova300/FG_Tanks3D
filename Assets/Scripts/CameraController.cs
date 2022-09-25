using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] GameObject idleCamera;
    private Transform target;
    public enum Mode{
        topview,
        topviewInverted,
        inheritAll
    }
    public Mode currentMode;
    private Quaternion mode0 = Quaternion.Euler(30,45,0);
    private Quaternion mode1 = Quaternion.Euler(30,-135,0);


    public void Start(){
        
    }
    void Update()
    {
        if (currentMode == Mode.topview){
            transform.rotation = mode0;
            transform.position = target.transform.position + new Vector3(-10, 10, -10);
        }
        if (currentMode == Mode.topviewInverted){
            transform.rotation = mode1;
            transform.position = target.transform.position + new Vector3(10, 10, 10);
        }
        if (currentMode == Mode.inheritAll){
            transform.rotation = target.transform.rotation;
            transform.position = target.transform.position;
        }
    }

    public void setTarget(Transform nTarget){
        target = nTarget;
    }

    public void setMode(Mode nMode){
        currentMode = nMode;
    }

    public void setCamera(Transform nTarget, Mode nMode){
        target = nTarget;
        currentMode = nMode;
    }

    public void setIdle(){
        target = idleCamera.transform;
        currentMode = Mode.inheritAll;
    }
}
