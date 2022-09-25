using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float force,r_force;
    [SerializeField] Rigidbody rocketBody;
    [SerializeField] Vector3 target;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float innacuracy;
    [SerializeField] private int fuse;
    private bool isActive;
    private int timer;
   
    public void Initialize(Vector3 r_target){
        Vector3 inaccuracy = new Vector3(Random.Range(-innacuracy, innacuracy), Random.Range(-innacuracy, innacuracy), Random.Range(-innacuracy, innacuracy));
        target = r_target + inaccuracy;
        isActive = true;
    }
    
    void FixedUpdate(){
        if (isActive){
            Vector3 direction = target - rocketBody.position;
            direction.Normalize();
            Vector3 r_Amount = Vector3.Cross(transform.forward, direction);
            rocketBody.angularVelocity = r_Amount * r_force;
            rocketBody.velocity = transform.forward * force;
            timer++;
            if(timer > fuse){
                isActive = false;
            }
        } else {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision){
        isActive = false;
    }
}
