﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

   Quaternion start, end;
   [SerializeField, Range(0.0f, 360f)]
   float angle = 90.0f;
   [SerializeField, Range(0.0f, 5.0f)]
   float speed = 2.0f;
   [SerializeField, Range(0.0f, 10.0f)]
   float startTime = 0.0f;




    // Start is called before the first frame update
    void Start()
    {
      start = PendulumRotation(angle);
      end = PendulumRotation(-angle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      startTime += Time.deltaTime;
      transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f) / 2.0f);
    }

   Quaternion PendulumRotation(float angle)
   {
      var pendulumRotation = transform.rotation;
      var angleZ = pendulumRotation.eulerAngles.z + angle;

      if(angleZ > 180)
      {
         angleZ -= 360;
      }
      else if(angleZ < 360)
      {
         angleZ += 360;
      }

      pendulumRotation.eulerAngles = new Vector3(pendulumRotation.eulerAngles.x, pendulumRotation.eulerAngles.y, angleZ);
      return pendulumRotation;
   }
}
