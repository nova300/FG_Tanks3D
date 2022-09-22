using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private PlayerAttrib playerAttrib;
    public bool currentMode, wait;
    public int cost;
    void Update()
    {
        if(playerTurn.IsPlayerTurn()){

            if(wait){
                if(agent.hasPath == false){
                    playerAttrib.deductAP(cost);
                    cost = 0;
                    wait = false;
                    Debug.LogError("done waiting");
                }
            }

            if(currentMode && !wait){
                
                agent.isStopped = true;
                
                if(agent.hasPath){
                    cost = (int)agent.remainingDistance;
                    Debug.Log(cost);
                }

                if(Input.GetMouseButtonDown(0)){
                    RaycastHit result;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out result, 100.0f)){
                        agent.SetDestination(result.point);
                    }
                }
                if(Input.GetMouseButtonDown(1)){
                    if(cost <= playerAttrib.getAP() && agent.hasPath){
                        agent.isStopped = false;
                        wait = true;
                        Debug.LogError("waiting");
                    }
                }
            }
        
        if(Input.GetKeyDown(KeyCode.V) && currentMode && !wait){
                agent.ResetPath();
                cost = 0;
                agent.isStopped = false;
                currentMode = false;
                Debug.Log("Movement mode off");
            } else if(Input.GetKeyDown(KeyCode.V) && !wait){
                currentMode = true;
                Debug.Log("Movement mode");
            }

        }
    }
}
