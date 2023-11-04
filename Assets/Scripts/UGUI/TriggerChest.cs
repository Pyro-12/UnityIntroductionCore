using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour
{
    [SerializeField] GameObject canvasInteraction;
    [SerializeField] GameObject mesh;//objeto que va a interacturar
    void Start()
    {
        canvasInteraction.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
         
        if (other.gameObject.tag == "Player")
        {
            canvasInteraction.SetActive(true);

            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("tecla");
                canvasInteraction.SetActive(false); //hacemos que desaparezca el canvas y luego el objeto
                Destroy(mesh);
            }
        }
    }
}
