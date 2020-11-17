using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      GameManager.instance.transitionScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
