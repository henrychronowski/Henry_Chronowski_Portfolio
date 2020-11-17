using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

   public float mouseSens = 100.0f;

   public Transform playerBody;

   float xRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked; // locks the mouse to the center of the screen
    }

    // Update is called once per frame
    void Update()
    {
      float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

      xRotation -= mouseY; //rotates the correct direction
      xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f); // clamps the rotation so that the players can look more then 180 degrees

      transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

      playerBody.Rotate(Vector3.up * mouseX);
    }
}
