using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] int mode;

    public void setMode(int nMode){
        mode = nMode;
    }

    public int getMode(){
        return mode;
    }

}
