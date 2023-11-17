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
    #endregion

    #region Auxiliary data
    // AUXILIARY DATA
    private bool allObjectsActivated;
    private Coroutine activationCoroutine; // Referencia a la corutina para poder detenerla si es necesario
    #endregion

    #region Initialize script
    private void Start()
    {
        // Suscribirse al evento de activación del objeto
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
        // Desuscribirse del evento al destruir el objeto
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
        // Si algún objeto se activa, reiniciamos el temporizador
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
        }

        activationCoroutine = StartCoroutine(ActivateObjectsWithDelay());
    }

    IEnumerator ActivateObjectsWithDelay()
    {
        allObjectsActivated = false;

        float timer = activationDelay;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            // Actualizar la barra de tiempo
            timerBar.fillAmount = timer / activationDelay;

            yield return null;
        }

        // Cuando se agota el tiempo, desactivar todos los objetos y reiniciar la barra
        DeactivateAllObjects();
        timerBar.fillAmount = 0f;

        yield return null;
    }

    void UnlockNextArea()
    {
        // animation 
        nextAreaLock.SetActive(false);
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
