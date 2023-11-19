using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootedObjectController : MonoBehaviour
{
    #region Data
    [Header("Countdown timer for activating all objects")]
    [SerializeField] float activationDelay = 5f;

    [Space(10)]
    [Header("Activation mechanics")]
    [SerializeField] GameObject nextAreaLock;
    [SerializeField] ShootableObjects[] shootableObjects;

    [Space(10)]
    [Header("UGUI Interface")]
    [SerializeField] Image timerBar;
    [SerializeField] GameObject timerCanvas;
    [Header ("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string activationTrigger = "isActive"; // Nombre del trigger de activación en el Animator
    #endregion

    #region Auxiliary data
    // AUXILIARY DATA
    [SerializeField]private bool allObjectsActivated;
    private Coroutine activationCoroutine; // Referencia a la corutina para poder detenerla si es necesario
    private bool isCoroutineRunning = false;

    #endregion

    #region Initialize script
    private void Start()
    {
        timerCanvas.SetActive(false);
            
        
        foreach (var shootableObject in shootableObjects)
        {
            if (shootableObject != null)
            {
                shootableObject.OnActivated += OnObjectActivated;
            }
        }
    }

    private void OnDestroy()
    {
        //Quit subscription when object's destroyed.
        foreach (var shootableObject in shootableObjects)
        {
            if (shootableObject != null)
            {
                shootableObject.OnActivated -= OnObjectActivated;
            }
        }
    }
    #endregion

    #region Logic Script
    private void OnObjectActivated()
    {
        // Evitar reiniciar la corutina innecesariamente
        if (!isCoroutineRunning)
        {
            // Si algún objeto se activa, reiniciamos el temporizador
            if (activationCoroutine != null)
            {
                StopCoroutine(activationCoroutine);
            }

            activationCoroutine = StartCoroutine(ActivateObjectsWithDelay());
        }
    }

    IEnumerator ActivateObjectsWithDelay()
    {
        isCoroutineRunning = true;

        timerCanvas.SetActive(true);
        allObjectsActivated = true;

        float timer = activationDelay;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            // Actualizar la barra de tiempo
            timerBar.fillAmount = timer / activationDelay;

            yield return null;
        }

        // Restaurar el estado de la corutina
        isCoroutineRunning = false;

        // Look if all objects are active prior to unlock next area.
        foreach (var shootableObject in shootableObjects)
        {
            if (shootableObject != null && !shootableObject.GetIsActive())
            {
                allObjectsActivated = false;
                break;
            }
        }

        if (allObjectsActivated)
        {
            Debug.Log("All objects activated. Triggering next action.");
            //When time's over and all objects are active, unlock next area.
            UnlockNextArea();
            animator.SetBool("isActive", true);
            Debug.Log("Animator set to true.");
        }
        else
        {
            Debug.Log("Not all objects activated. Resetting timer.");
            //If not all objects are activated, deactivate the objects and restart de bar coroutine for timer.
            DeactivateAllObjects();
            timerBar.fillAmount = 0f;
            timerCanvas.SetActive(false);
            // SetActiveState(false);
            animator.SetBool("isActive", false);
            Debug.Log("Animator set to false.");
        }

        yield return null;
    }

    void UnlockNextArea()
    {
        // animation 
        //nextAreaLock.SetActive(false);
        timerCanvas.SetActive(false);
        animator.SetBool("isActive", true);
        Debug.Log(animator);
    }

    void DeactivateAllObjects()
    {
        foreach (var shootableObject in shootableObjects)
        {
            if (shootableObject != null)
            {
                shootableObject.DeactivateObject();
            }
        }
    }

    #endregion
}
