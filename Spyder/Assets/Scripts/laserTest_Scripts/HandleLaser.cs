using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleLaser : MonoBehaviour
{
    // * Variables *
    KillBeam actualLaser;
    Image laserTimer;

    public float maxTime; // This is roughly in seconds.
    float time = 0;
    bool laserUp = true;

    public float pauseTime; // ditto above.
    float pause = 0;

    // ** Update Functions **
    private void Start()
    {
        //actualLaser = gameObject.GetComponentInChildren<KillBeam>();
        actualLaser = gameObject.transform.Find("LaserBeam").GetComponent<KillBeam>();
        laserTimer = gameObject.transform.Find("LaserCanvas").Find("LaserTimer").GetComponent<Image>();

        time = maxTime;
    }

    private void Update()
    {
        Timer();
    }

    // **** Other Functions ****
    void Timer()
    {
        if (pause > 0) // pause on turn off/on
        {
            pause -= Time.deltaTime;
        }


        if (laserUp == false && pause <= 0) // timing for the laser
        {
            time += Time.deltaTime;
            laserTimer.fillAmount = time / maxTime;
        } 
        else if (pause <= 0)
        {
            time -= Time.deltaTime;
            laserTimer.fillAmount = time / maxTime;
        }
        

        if (time / maxTime >= 1 && pause <= 0)
        {
            laserUp = true;
            actualLaser.EnableDisable();
            pause = pauseTime;
        }

        if (time / maxTime <= 0 && pause <= 0)
        {
            laserUp = false;
            actualLaser.EnableDisable();
            pause = pauseTime; // there should be a better way than this duplication
        }
    }


}
