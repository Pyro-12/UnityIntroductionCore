using System.Collections;
using UnityEngine;

public class ShootedObjectController : MonoBehaviour
{
    #region Data
    [SerializeField] float activationDelay = 5f;
    [SerializeField] GameObject nextAreaLock;
    [SerializeField] ShootableObjects[] shootableObjects;
    #endregion

    #region Auxiliary data
    //AUXILIARY DATA
    private bool allObjectsActivated;
    #endregion

    #region Initialize script
    private void Start()
    {
        StartCoroutine(ActivateObjectsWithDelay());
        GetComponent<ShootableObjects>();
    }
    #endregion
    #region Logic Script
    IEnumerator ActivateObjectsWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        allObjectsActivated = true;

        foreach (var shootableObject in shootableObjects)
        {
            if (shootableObject != null && !shootableObject.GetIsActive())
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
            // When all aboject's been activated unlock the next zone
            UnlockNextArea();
        }
    }

    void UnlockNextArea()
    {
        //animation 
        nextAreaLock.SetActive(false);

    }
    #endregion
}
