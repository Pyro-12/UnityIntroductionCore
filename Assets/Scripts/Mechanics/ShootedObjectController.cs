using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootedObjectController : MonoBehaviour
{
    [SerializeField] float activationDelay = 5f;
    [SerializeField] ShootableObject[] shootableObjects;

    private bool allObjectsActivated;

    private void Start()
    {
        StartCoroutine(ActivateObjectsWithDelay());
    }

    IEnumerator ActivateObjectsWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        allObjectsActivated = true;

        foreach (var shootableObject in shootableObjects)
        {
            if (shootableObject != null && !shootableObject.isActive)
            {
                allObjectsActivated = false;
                break;
            }
        }

        if (!allObjectsActivated)
        {
            Debug.Log("Not all objects activated. Resetting timer.");
            StartCoroutine(ActivateObjectsWithDelay());
        }
        else
        {
            Debug.Log("All objects activated. Triggering next action.");
            // Coloca aquí la lógica para activar la puerta o realizar la siguiente acción.
        }
    }
}
