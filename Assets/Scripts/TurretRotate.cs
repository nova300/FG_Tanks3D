using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    [SerializeField] PlayerAttrib playerAttrib;
    private RaycastHit result;
    void Update()
    {
        if (playerAttrib.IsPlayerTurn()){
        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool uiBlock = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if(Physics.Raycast(ray, out result, 100.0f) && !uiBlock){
                Vector3 direction = result.point - transform.position;
                direction.Normalize();
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f);      
            } 
        }
    }
}
