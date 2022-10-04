using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToGround : MonoBehaviour

{
    [SerializeField] PlayerActions playerActions;
    void FixedUpdate()
    {
        if (playerActions == null){
            Align();
        } else if (playerActions.IsMoving()){
            Align();
        }
    }

    void Align(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit)){
                var slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);
                transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * transform.rotation, 10 * Time.deltaTime);
        }
    }
}
