using UnityEngine;

public class ShootableObjects : MonoBehaviour
{
    #region Data
    [Header("Visuals")]
    [SerializeField] [Tooltip ("Activated object shows particles to indicate the state to the player")]ParticleSystem activatedObjectFX;
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA
    bool isActive;
    #endregion
    #region Initialize script
    private void Start()
    {
        activatedObjectFX.Stop();
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
            //ACTIVAR ANIMACION
        }
        Debug.Log(gameObject.name + " is now active.");
    }
    #endregion
}
