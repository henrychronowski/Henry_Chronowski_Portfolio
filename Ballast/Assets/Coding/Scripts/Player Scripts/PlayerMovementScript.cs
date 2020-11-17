using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

   public CharacterController controller;

   public float speed;
   float walkSpeed = 12.0f;
   float crouchSpeed;
  
   bool isCrouched;

   public float gravity = -9.81f;

   Vector3 velocity;

   void Start()
   {
      walkSpeed = speed;
      crouchSpeed = speed / 2;
   }

   // Update is called once per frame
   void Update()
    {

      float x = Input.GetAxis("Horizontal");
      float z = Input.GetAxis("Vertical");

      Vector3 move = transform.right * x + transform.forward * z;

      controller.Move(move * speed * Time.deltaTime);

      if (Input.GetKeyDown(KeyCode.LeftControl))
      {
         if(isCrouched == false)
         {
            speed = crouchSpeed;
            controller.height -= 2f;
            isCrouched = true;
         }
         else
         {
            speed = walkSpeed;
            controller.height += 2f;
            isCrouched = false;
         }         
      }

      velocity.y += gravity * Time.deltaTime;

      controller.Move(velocity * Time.deltaTime);
    }

   public void resetVelocity()
   {
      velocity.y = -2f;
   }
}
