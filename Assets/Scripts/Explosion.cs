using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Awake(){
        Destroy(gameObject, 2.0f);
    }
    private void OnTriggerEnter(Collider other){
        GameObject otherObject = other.gameObject;
        if(otherObject.TryGetComponent(out PlayerAttrib test)){
            int damage = (int)(10 / Vector3.Distance(gameObject.transform.position, otherObject.transform.position) / Random.Range(0.8f, 1.2f));
            Debug.Log(damage);
            otherObject.GetComponent<PlayerAttrib>().Damage(damage);
        }
    }
}
