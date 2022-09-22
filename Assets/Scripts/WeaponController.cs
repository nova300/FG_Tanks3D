using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform offset;
    [SerializeField] float r_speed,r_upForce;
    [SerializeField] int r_burnTime;
    void Update()
    {
        bool IsPlayerTurn = playerTurn.IsPlayerTurn();
        if(IsPlayerTurn){
            if (Input.GetKeyDown(KeyCode.Space)){
                GameObject newProjectile = Instantiate(projectile);
                newProjectile.transform.position = offset.position;
                newProjectile.transform.rotation = transform.rotation;
                newProjectile.GetComponent<Rocket>().Initialize(r_speed, r_upForce, r_burnTime);
            }
        }
    }
}
