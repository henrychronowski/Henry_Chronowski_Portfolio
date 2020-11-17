using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   public GameObject fToInteract;

   public Transform onHand;
   Rigidbody item;

   public LayerMask playermask;

   public float zoneRadius = 1.0f;


   bool isInRadius;
   bool isInHand = false;

   private void Start()
   {
      fToInteract.SetActive(false);
      item = gameObject.GetComponent<Rigidbody>();
   }
   // Update is called once per frame
   void Update()
   {

      isInRadius = Physics.CheckSphere(gameObject.transform.position, zoneRadius, playermask);



      if (isInRadius == true)
      {
         fToInteract.SetActive(true);
      }
      else
      {
         fToInteract.SetActive(false);
      }


      if (Input.GetKeyDown("f") && isInRadius == true && isInHand == false)
      {
         isInHand = true;
         item.useGravity = false;
         transform.position = onHand.position;
         transform.parent = GameObject.Find("First Person Player").transform;
         item.constraints = RigidbodyConstraints.FreezeAll;
      }
      else if (Input.GetKeyUp("f") && isInHand == true)
      {
         isInHand = false;
         transform.parent = null;
         item.useGravity = true;
         item.constraints = RigidbodyConstraints.None;
      }

   }
}