using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRotate : MonoBehaviour
{
    [SerializeField] PlayerAttrib playerAttrib;
    bool isActive = false;
    Vector3 point;

    void Update(){
        if (playerAttrib.IsPlayerTurn() && isActive){
            Vector3 direction = point - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0;
            lookRotation.z = 0;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f);  
            
            transform.rotation = newRotation;
            Debug.Log(direction.y);
            Debug.Log(transform.rotation.y);
            if (transform.rotation.y == direction.y){
                
                isActive = false;
            }
            
        }
    }

    public void SetRotation(Vector3 rotationTarget){
        point = rotationTarget;
        isActive = true;
        
    }
}
