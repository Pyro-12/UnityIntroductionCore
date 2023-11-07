using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjects : MonoBehaviour
{
    #region Auxiliary data
    bool isActive;
    #endregion

    public virtual void ActivateObject()
    {
        isActive = true;
        Debug.Log(gameObject.name + " is now active.");
    }
}
