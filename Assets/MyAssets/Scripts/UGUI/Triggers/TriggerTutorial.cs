using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    #region Data
    [Header("UGUI")]
    [SerializeField] GameObject canvasTutorial;
    [SerializeField] GameObject mesh;//objeto que va a llevar el panel
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA
    [SerializeField] bool isActive;
    #endregion
    #region Logic Script
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvasTutorial.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            canvasTutorial.SetActive(false);
        }
    }
    #endregion
}
