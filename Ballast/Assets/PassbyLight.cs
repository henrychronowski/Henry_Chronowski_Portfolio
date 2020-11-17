using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassbyLight : MonoBehaviour
{
   [SerializeField]
   float eulerAngY;

   public float LightIntensity = 3.0f;
   public float speed = 10;
   // Start is called before the first frame update
   void Start()
    {
      gameObject.GetComponent<Light>().intensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
      eulerAngY = transform.localEulerAngles.y;
      gameObject.transform.Rotate(0, eulerAngY * Time.deltaTime * speed, 0);

    }
}
