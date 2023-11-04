using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    [SerializeField] GameObject canvasTutorial;
    [SerializeField] bool isActive;
    [SerializeField] GameObject mesh;//objeto que va a llevar el panel

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
}
