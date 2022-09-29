using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject idleCamera;
    private Transform target;
    void Update(){
        transform.rotation = target.transform.rotation;
        transform.position = target.transform.position;
    }
    public void setCamera(Transform nTarget){
        target = nTarget;
    }

    public void setIdle(){
        target = idleCamera.transform;
    }
}
