using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed,upForce;
    [SerializeField] private int burnTime = -1;
    [SerializeField] Rigidbody rocketBody;
    private bool isActive;
    public void Initialize(float r_speed, float r_upForce, int r_burnTime){
        burnTime = r_burnTime;
        speed = r_speed;
        upForce = r_upForce;
        isActive = true;
    }

    
    void Update(){
        if (isActive && !(burnTime == 0)){
            rocketBody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime) + (transform.up * upForce * Time.deltaTime));
            burnTime = burnTime - 1;
        } else if(isActive){
            isActive = false;
        }
    }

    private void OnCollisionEnter(Collision collision){
        isActive = false;
    }
}
