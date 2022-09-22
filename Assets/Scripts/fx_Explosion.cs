using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fx_Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

}
