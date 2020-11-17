using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : MonoBehaviour
{
   public Transform playerController;
   public PlayerMovementScript playerInput;
   public bool inside = false;
   public float ladderHeight = 3.2f;

    // Start is called before the first frame update
    void Start()
    {
      playerInput = GetComponent<PlayerMovementScript>();
    }

   private void OnTriggerEnter(Collider other)
   {
      if(other.gameObject.tag == "Ladder")
      {
         playerInput.enabled = false;
         inside = !inside;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.tag == "Ladder")
      {
         playerInput.enabled = true;
         inside = !inside;
         playerInput.resetVelocity();
      }
   }

   // Update is called once per frame
   void Update()
   {
        if(inside == true && Input.GetKey("w"))
        {
         playerController.transform.position += Vector3.up / ladderHeight;
        }
    }
}
