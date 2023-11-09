using UnityEngine;

public class ShootableObjects : MonoBehaviour
{
    #region Data
    [Header("Visuals")]
    [SerializeField] [Tooltip ("Activated object shows particles to indicate the state to the player")]ParticleSystem activatedObjectFX;
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string activationTrigger = "Activate"; // Nombre del trigger de activaci�n en el Animator
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA
    bool isActive=false;
    #endregion
    #region Initialize script
    private void Start()
    {
        activatedObjectFX.Stop();
        isActive = false;
        SetActiveState(false); // Asegurarse de que el objeto est� inicialmente desactivado
    }
    #endregion

    #region Logic Script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("ay!");
            // Si el objeto recibe un disparo, activa la VFX y la animaci�n
            ActivateObject();
        }
    }

    public virtual void ActivateObject()
    {
        isActive = true;

        if (isActive == true)
        {
            activatedObjectFX.Play();
            SetActiveState(true); // Activa la animaci�n
        }   
    }

    // M�todo para cambiar el estado de la animaci�n en el Animator
    private void SetActiveState(bool state)
    {
        if (animator != null)
        {
            // Utiliza el trigger para activar la animaci�n
            animator.SetTrigger(activationTrigger);
        }
    }
    #endregion
}
