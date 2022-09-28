using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] public PlayerAttrib playerAttrib;
    [SerializeField] private GameObject rocket,smoke,explosion,indicator;
    [SerializeField] private Transform offset, barrel, camRotation;
    [SerializeField] private int rocketCost=1, atrifleCost=5, atrifleDamage=30;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioSource sndCannon;
    private int moveCost;
    private GameObject moveIndicator;

    public void Start(){
        agent.isStopped = true;
    }
    public void Update(){
        if(agent.hasPath && agent.isStopped){
            moveCost = (int)agent.remainingDistance;
            if (moveCost <= playerAttrib.getAP()){
                playerAttrib.turnManager.hud.setMoveCost(moveCost);
                if (moveIndicator == null){
                    moveIndicator = Instantiate(indicator);
                    moveIndicator.transform.position = agent.destination;
                } else {
                    moveIndicator.transform.position = agent.destination;
                }
            } else  {
                playerAttrib.turnManager.hud.setMoveCost(0);
                destroyMoveIndicator();
            }  
        } else if(!agent.hasPath) {
            destroyMoveIndicator();
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
                GameObject otherObject = rifleResult.transform.gameObject;
                if(otherObject.TryGetComponent(out PlayerAttrib test)){
                    otherObject.GetComponent<PlayerAttrib>().damage(atrifleDamage);
                    GameObject newExplosion = Instantiate(explosion);
                    newExplosion.transform.position = rifleResult.point;
                }
            GameObject hitSmoke = Instantiate(smoke);
            hitSmoke.transform.position = rifleResult.point;
            sndCannon.Play();
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
            playerAttrib.turnManager.hud.setMoveCost(0);
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
        playerAttrib.turnManager.hud.setMoveCost(moveCost);
    }

    public void destroyMoveIndicator(){
        if (!(moveIndicator == null)){
            Destroy(moveIndicator);
        }
    }

    public void rotateCam(float speed){
        camRotation.Rotate(0, speed * Time.deltaTime, 0);
    }
}
