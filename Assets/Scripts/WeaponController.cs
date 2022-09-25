using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private PlayerAttrib playerAttrib;
    [SerializeField] private GameObject rocket,smoke,muzzleflash,rifleHit;
    [SerializeField] private Transform offset, barrel;
    [SerializeField] private int rocketCost=1, atrifleCost=5, atrifleDamage=30;
    void Update()
    {
        bool IsPlayerTurn = playerTurn.IsPlayerTurn();
        if(IsPlayerTurn){


            int mode = playerAttrib.getMode();
            bool wait = playerAttrib.isWaiting();

            if(Input.GetMouseButtonDown(0) && mode == 2 && !wait && playerAttrib.apIsMoveAllowed(rocketCost)){
                    RaycastHit result;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out result, 100.0f)){
                        playerAttrib.deductAP(rocketCost);
                        GameObject newRocket = Instantiate(rocket);
                        newRocket.transform.position = offset.position;
                        newRocket.transform.rotation = offset.rotation;
                        
                        newRocket.GetComponent<Rocket>().Initialize(result.point);

                        GameObject newSmoke = Instantiate(smoke);
                        newSmoke.transform.position = offset.position;

                    }
                }

            if(Input.GetMouseButtonDown(0) && mode == 3 && !wait && playerAttrib.apIsMoveAllowed(atrifleCost)){
                    RaycastHit result;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out result, 100.0f)){
                        playerAttrib.deductAP(atrifleCost);
                        GameObject newSmoke = Instantiate(smoke);
                        newSmoke.transform.position = barrel.position;
                        
                        RaycastHit rifleResult;
                        Vector3 direction = result.point - barrel.transform.position;
                        bool hit = Physics.Raycast(barrel.transform.position, direction, out rifleResult, 100.0f);
                        if(hit){
                            GameObject otherObject = result.transform.gameObject;
                            if(otherObject.TryGetComponent(out PlayerAttrib test)){
                                otherObject.GetComponent<PlayerAttrib>().damage(atrifleDamage);
                            }
                            GameObject hitSmoke = Instantiate(smoke);
                            hitSmoke.transform.position = rifleResult.point;
                        }

                    }
                }

            
        
            if(Input.GetKeyDown(KeyCode.B) && mode == 2 && !wait){
                    playerAttrib.setMode(0);
                    Debug.Log("weapon mode off");
                } else if(Input.GetKeyDown(KeyCode.B) && !wait){
                    playerAttrib.setMode(2);
                    Debug.Log("weapon mode: rocket bomb");
                }
            
            if(Input.GetKeyDown(KeyCode.N) && mode == 3 && !wait){
                    playerAttrib.setMode(0);
                    Debug.Log("weapon mode off");
                } else if(Input.GetKeyDown(KeyCode.N) && !wait){
                    playerAttrib.setMode(3);
                    Debug.Log("weapon mode: at rifle");
                }
        }
    }



}
