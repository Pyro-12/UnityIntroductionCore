using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    #region Data
    [Header("Sesitivity mouse")]
    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    [Space(10)][SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA
    float mouseX, mouseY;
    float xRotation = 0f;
    #endregion

    #region Script's logic
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
        
    }

    void ReceiveInput ()
    {
        //mouseX = mouseInput.x * sensitivityX;
        //mouseX = mouseInput.y * sensitivityY;
    }
    #endregion
}
