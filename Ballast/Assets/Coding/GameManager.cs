using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public GameObject[] ghostLights;
   public GameObject[] doorButtons;
   float lightIntensity;
   public GameObject SoundManager;
   public GameObject dialogTrigger;
    public static GameManager instance = null;
   public Light LightSwitchConfrim;
   Color newLightColor;
   public Text HullUI;
    public float hullDamage = 1000;
   public bool powerIsOn;
   // Start is called before the first frame update
   void Start()
    {
      powerIsOn = false;
      ghostLights = GameObject.FindGameObjectsWithTag("GhostLights");
      doorButtons = GameObject.FindGameObjectsWithTag("DoorButtons");
      lightIntensity = ghostLights[0].GetComponent<PassbyLight>().LightIntensity;

      SoundManager.SetActive(false);
      dialogTrigger.SetActive(false);

      if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

   public void Update()
   {

      if(Input.GetKeyDown(KeyCode.Escape))
      {
         transitionScene(0);
      }

      if (hullDamage > 0)
      {
         HullUI.text = hullDamage.ToString();
      }

      if(hullDamage <= 0)
      {
         transitionScene(3);
      }
   }

   public void confirmLightsOn()
   {
      newLightColor.r = 30.0f/255f;
      newLightColor.g = 164.0f/255f;
      newLightColor.b = 30.0f/255f;

      LightSwitchConfrim.color = newLightColor;
      
   }
   public void worldTriggersWhenLightsTurnOn()
   {
      for (int i = 0; i < ghostLights.Length; i++)
      {
         ghostLights[i].GetComponent<Light>().intensity = lightIntensity;
      }
      for (int i = 0; i < doorButtons.Length; i++)
      {
         doorButtons[i].GetComponent<ButtonToOpenDoor>().isOn = true;
      }
      SoundManager.SetActive(true);
      dialogTrigger.SetActive(true);
   }


    public float getHullDamage()
    {
        return hullDamage;
    }

    public void dealDamage(float x)
    {
        hullDamage -= x;
    }


   public void StartTimer(float duration)
   {
      StartCoroutine(RunTimer(duration));
   }

   private IEnumerator RunTimer(float duration)
   {
      yield return new WaitForSeconds(duration);
   }

   public void transitionScene(int x)
   {
      SceneManager.LoadScene(x);
      instance = null;
      Destroy(gameObject);
   }
}
