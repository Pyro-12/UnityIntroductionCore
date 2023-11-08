using UnityEngine;

public class ShootableObjects : MonoBehaviour
{
    #region Data
    [Header("Visuals")]
    [SerializeField] [Tooltip ("Activated object shows particles to indicate the state to the player")]ParticleSystem activatedObjectFX;
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string activationTrigger = "Activate"; // Nombre del trigger de activación en el Animator
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA
    bool isActive;
    #endregion
    #region Initialize script
    private void Start()
    {
        activatedObjectFX.Stop();
        SetActiveState(false); // Asegurarse de que el objeto esté inicialmente desactivado
    }
    #endregion
    #region Logic Script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("ay!");
            //if object's get hit activate the vfx and counter in controller
            ActivateObject();
        }
    }
    public virtual void ActivateObject()
    {
        isActive = true;

        if (isActive == true)
        {
            activatedObjectFX.Play();
            SetActiveState(true); // Activa la animación

        }
    }

    // Método para cambiar el estado de la animación en el Animator
    private void SetActiveState(bool state)
    {
        if (animator != null)
        {
            animator.SetBool(activationTrigger, state);
        }
    }
    #endregion
}
