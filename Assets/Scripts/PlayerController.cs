using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private PlayerTurn playerTurn;
    public bool currentMode;
    public int cost;
    void Update()
    {
        if(playerTurn.IsPlayerTurn()){
            if(currentMode){
                
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
                    if(cost < 11 && agent.hasPath){
                        //deduct cost from player ap
                        cost = 0;
                        agent.isStopped = false;
                        currentMode = false;
                    }
                }
            }
        
        if(Input.GetKeyDown(KeyCode.V) && currentMode){
                agent.ResetPath();
                cost = 0;
                agent.isStopped = false;
                currentMode = false;
                Debug.Log("Movement mode off");
            } else if(Input.GetKeyDown(KeyCode.V)){
                currentMode = true;
                Debug.Log("Movement mode");
            }

        }
    }
}
