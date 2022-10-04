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
    public void SetCamera(Transform nTarget){
        target = nTarget;
    }

    public void SetIdle(){
        target = idleCamera.transform;
    }
}
