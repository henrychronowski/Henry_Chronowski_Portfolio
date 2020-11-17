using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // * Variables *
    Transform pos;
    Vector3 start;


    // ** Update Functions **
    private void Start()
    {
        pos = gameObject.transform;
        start = pos.position;
    }


    // **** Other Functions ****
    public void GetKilled ()
    {
        pos.position = start;
    }

}
