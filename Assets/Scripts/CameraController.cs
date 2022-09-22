using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraTarget target;
    [SerializeField] int mode;  /* mode 0 = 30dgr top view, mode 1 = 30dgr top view invert, mode 2 = inherit all */
    private Quaternion mode0 = Quaternion.Euler(30,45,0);
    
    private Quaternion mode1 = Quaternion.Euler(30,-135,0);
    void Update()
    {
        if (mode == 0){
            transform.rotation = mode0;
            transform.position = target.transform.position + new Vector3(-10, 10, -10);
        }
        if (mode == 1){
            transform.rotation = mode1;
            transform.position = target.transform.position + new Vector3(10, 10, 10);
        }
        if (mode == 2){
            transform.rotation = target.transform.rotation;
            transform.position = target.transform.position;
        }
    }

    public void setTarget(CameraTarget nTarget){
        target = nTarget;
    }

    public void setMode(int nMode){
        mode = nMode;
    }
}
