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
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private AudioSource sndCannon;
    [SerializeField] private TankRotate tankRotate;
    private int moveCost;
    private GameObject moveIndicator;

    public void Start(){
        agent.isStopped = true;
    }
    public void Update(){
        if(agent.hasPath && agent.isStopped){
            moveCost = (int)(6 * (agent.remainingDistance / 10));
            if (playerAttrib.ApIsMoveAllowed(moveCost)){
                playerAttrib.turnManager.hud.SetMoveCost(moveCost);
                if (moveIndicator == null){
                    moveIndicator = Instantiate(indicator);
                    moveIndicator.transform.position = agent.destination;
                } else {
                    moveIndicator.transform.position = agent.destination;
                }
            } else  {
                playerAttrib.turnManager.hud.SetMoveCost(0);
                DestroyMoveIndicator();
            }  
        } else if(!agent.hasPath) {
            DestroyMoveIndicator();
            if (!agent.isStopped){
                playerAttrib.DeductAP(moveCost);
                agent.isStopped = true;
            }
            
        }
    }


    public void ShootRocket(RaycastHit result){
        if(playerAttrib.ApIsMoveAllowed(rocketCost)){
            playerAttrib.DeductAP(rocketCost);
            GameObject newRocket = Instantiate(rocket);
            newRocket.transform.position = offset.position;
            newRocket.transform.rotation = offset.rotation;
            newRocket.GetComponent<Rocket>().Initialize(result.point);
            GameObject newSmoke = Instantiate(smoke);
            newSmoke.transform.position = offset.position;
        }
    }

    public void ShootShell(RaycastHit result){
        if(playerAttrib.ApIsMoveAllowed(atrifleCost)){
            playerAttrib.DeductAP(atrifleCost);
            GameObject newSmoke = Instantiate(smoke);
            newSmoke.transform.position = barrel.position;
            
            RaycastHit rifleResult;
            Vector3 direction = result.point - barrel.transform.position;
            bool hit = Physics.Raycast(barrel.transform.position, direction, out rifleResult, 100.0f);
            if(hit){
                GameObject otherObject = rifleResult.transform.gameObject;
                if(otherObject.TryGetComponent(out PlayerAttrib test)){
                    otherObject.GetComponent<PlayerAttrib>().Damage(atrifleDamage);
                    GameObject newExplosion = Instantiate(explosion);
                    newExplosion.transform.position = rifleResult.point;
                }
            GameObject hitSmoke = Instantiate(smoke);
            hitSmoke.transform.position = rifleResult.point;
            sndCannon.Play();
            }
        }
    }


    public void SetDestination(RaycastHit result){
        tankRotate.SetRotation(result.point);
        agent.isStopped = true;
        agent.SetDestination(result.point);
    }

    public void GoDestination(){
        if(playerAttrib.ApIsMoveAllowed(moveCost)){
            playerAttrib.turnManager.hud.SetMoveCost(0);
            agent.isStopped = false;
        }
    }

    public bool IsMoving(){
        if(agent.hasPath && !agent.isStopped){
            return true;
        } else {
            return false;
        }
    }

    public void CleanExitMoveMode(){
        agent.isStopped = true;
        agent.ResetPath();
        moveCost = 0;
        playerAttrib.turnManager.hud.SetMoveCost(moveCost);
    }

    public void DestroyMoveIndicator(){
        if (!(moveIndicator == null)){
            Destroy(moveIndicator);
        }
    }

    public void RotateCam(float speed){
        camRotation.Rotate(0, speed * Time.deltaTime, 0);
    }
}
