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
    bool isActive=false;
    public event System.Action OnActivated;
    #endregion
    #region Initialize script
    private void Start()
    {
        activatedObjectFX.Stop();
        isActive = false;
        SetActiveState(false); // Asegurarse de que el objeto esté inicialmente desactivado
    }
    #endregion
    // Método público para obtener el valor de isActive
    public bool GetIsActive()
    {
        return isActive;
    }
    #region Logic Script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("ay!");
            // Si el objeto recibe un disparo, activa la VFX y la animación
            ActivateObject();
        }
    }

    public  void ActivateObject()
    {
        isActive = true;

        if (isActive == true)
        {
            activatedObjectFX.Play();
            SetActiveState(true); // Activa la animación
        }   
    }

    public  void DeactivateObject()
    {
        isActive = false;
        if (isActive == false)
        {
            activatedObjectFX.Stop();
            SetActiveState(false);
        }
    }

    // Método para cambiar el estado de la animación en el Animator
    private void SetActiveState(bool state)
    {
        if (animator != null)
        {
            // Utiliza el trigger para activar la animación
            //animator.SetTrigger(activationTrigger);

            // Notificar que el objeto se ha activado
            if (state)
            {
                OnActivated?.Invoke();
            }
        }
    }
    #endregion
}
