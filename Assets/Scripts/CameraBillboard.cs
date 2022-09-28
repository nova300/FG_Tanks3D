using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
  [SerializeField] bool lockZ = true;
   private void LateUpdate()
     {
        if (lockZ){
          transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
        } else {
          transform.forward = Camera.main.transform.forward;
        }
     }
}
