using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBeam : MonoBehaviour
{
    // * Variables *
    SpriteRenderer spr;
    BoxCollider2D bxCl;


    // ** Update Functions **
    private void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        bxCl = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KillPlayer>().GetKilled();
        }
    }

    // **** Other Functions ****
    public void EnableDisable() // Turns on & off the laser
    {
        if (spr.enabled == true)
        {
            spr.enabled = false;
            bxCl.enabled = false;
        } 
        else
        {
            spr.enabled = true;
            bxCl.enabled = true;
        }
    }

}
