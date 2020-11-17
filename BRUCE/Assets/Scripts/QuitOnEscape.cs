using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henry's quick and stupid way to deal with the game manager not being everywhere
public class QuitOnEscape : MonoBehaviour
{
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
