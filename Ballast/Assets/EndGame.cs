using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
   public GameObject StaticWheel;
   public GameObject endingSequence;
    // Start is called before the first frame update
    void Start()
    {
      StaticWheel.SetActive(false);
    }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == ("EndTrigger"))
      {
         Destroy(gameObject);
         StaticWheel.SetActive(true);
         endingSequence.SetActive(true);
      }
   }
   // Update is called once per frame
   void Update()
    {
        
    }
}
