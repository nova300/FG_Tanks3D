using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerAttrib playerAttrib;
    [SerializeField] private GameObject rocket,smoke;
    [SerializeField] private Transform offset, barrel;
    [SerializeField] private int rocketCost=1, atrifleCost=5, atrifleDamage=30;
    [SerializeField] private NavMeshAgent agent;
    private int moveCost;

    public void Start(){
        agent.isStopped = true;
    }
    public void Update(){
        if(agent.hasPath && agent.isStopped){
            moveCost = (int)agent.remainingDistance;
        }
    }


    public void shootRocket(RaycastHit result){
        if(playerAttrib.apIsMoveAllowed(rocketCost)){
            playerAttrib.deductAP(rocketCost);
            GameObject newRocket = Instantiate(rocket);
            newRocket.transform.position = offset.position;
            newRocket.transform.rotation = offset.rotation;
            newRocket.GetComponent<Rocket>().Initialize(result.point);
            GameObject newSmoke = Instantiate(smoke);
            newSmoke.transform.position = offset.position;
        }
    }

    public void shootShell(RaycastHit result){
        if(playerAttrib.apIsMoveAllowed(atrifleCost)){
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


    public void setDestination(RaycastHit result){
        agent.isStopped = true;
        agent.SetDestination(result.point);
    }

    public void goDestination(){
        if(playerAttrib.apIsMoveAllowed(moveCost)){
            playerAttrib.deductAP(moveCost);
            agent.isStopped = false;
        }
    }

    public bool isMoving(){
        if(agent.hasPath && !agent.isStopped){
            return true;
        } else {
            return false;
        }
    }

    public void cleanExitMoveMode(){
        agent.isStopped = true;
        agent.ResetPath();
        moveCost = 0;
    }
}
