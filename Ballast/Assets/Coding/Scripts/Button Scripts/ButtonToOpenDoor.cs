using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonToOpenDoor : MonoBehaviour
{
   public GameObject fToInteract;
   public GameObject door;
   public LayerMask playermask;

   public float zoneRadius = 1.0f;
   float startingY;

   float timer;
   public float timeDoorIsOpen = 3.0f;

   public int doorOpeningSpeed = 10;
   public int doorClosingSpeed;

   bool doorOpening;
   bool doorsOpen;
   bool doorClosing;
   bool isInRadius;
   public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
      fToInteract.SetActive(false);
      startingY = door.transform.position.y;
      doorClosingSpeed = doorOpeningSpeed;
    }

    // Update is called once per frame
    void Update()
    {

      isInRadius = Physics.CheckSphere(gameObject.transform.position, zoneRadius, playermask);

      if(isOn == true)
      {
         if (isInRadius == true)
         {
            fToInteract.SetActive(true);
         }
         else
         {
            fToInteract.SetActive(false);
         }

         if (Input.GetKeyDown("f") && isInRadius == true && !doorsOpen)
         {
            doorOpening = true;
         }

         if (doorOpening == true)
         {
            door.transform.Translate(Vector3.up * Time.deltaTime * doorOpeningSpeed);
         }

         if (door.transform.position.y > 11.0f)
         {
            doorOpening = false;
            doorsOpen = true;
            timer += Time.deltaTime;

            if (timer >= timeDoorIsOpen)
            {
               doorClosing = true;
               timer = 0.0f;
            }
         }

         if (doorClosing == true)
         {
            door.transform.Translate(Vector3.down * Time.deltaTime * doorClosingSpeed);
         }

         if (door.transform.position.y < startingY)
         {
            doorClosing = false;
            doorsOpen = false;
         }
      }
      
   }
}
