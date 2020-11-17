using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
   public GameObject anim;
   bool playerInRange;
    public LayerMask PlayerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      playerInRange = Physics.CheckSphere(transform.position, 2, PlayerLayer);
      if(playerInRange)
      {
         anim.SetActive(true);
      }
    }
}
