using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
   public GameObject fToInteract;

   public GameObject[] Lighting;
   public LayerMask playermask;
   public float zoneRadius = 1.0f;
   bool firstRun = true;
   bool isInRadius;

   // Start is called before the first frame update
   void Start()
    {
      fToInteract.SetActive(false);
      Lighting = GameObject.FindGameObjectsWithTag("Lights");

      for (int i = 0; i < Lighting.Length; i++)
      {
         Lighting[i].SetActive(false);
      }
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

      if (Input.GetKeyDown("f") && isInRadius == true && GameManager.instance.powerIsOn == false)
        {
            if(firstRun == true)
            {
               firstRun = false;
               GameManager.instance.confirmLightsOn();
               GameManager.instance.worldTriggersWhenLightsTurnOn();
            }
            for (int i = 0; i < Lighting.Length; i++)
            {
                Lighting[i].SetActive(true);

                //GameManager.instance.StartTimer(1);
            }
            GameManager.instance.powerIsOn = true;
        }
        
   }
}
