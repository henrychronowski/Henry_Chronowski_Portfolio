using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void transitionGame()
   {
      SceneManager.LoadScene("FINAL");
   }

   public void TransitionCredits()
   {
      SceneManager.LoadScene("CREDITS");
   }

   public void TransitionMain()
   {
      SceneManager.LoadScene("MainMenu");
   }

   public void quitGame()
    {
        Application.Quit();
    }
}
