using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    #region Data
    [Header ("Shoot")]
    [SerializeField] GameObject projectile;
    [SerializeField] float speed;
    [SerializeField] bool shootPressed;
    [Space(10)][Header("Weapon stats")]
    [SerializeField] float range = 50;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentAmmo = 10;
    [SerializeField] float fireRate = 100f;
    [Header("Postpo")]
    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private AudioSource shootAudioSource;
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA

    [SerializeField] bool canShoot;
    [SerializeField] bool isReloading;
    #endregion
    #region
    private void Start()
    {
        isReloading = false;

    }
    private void OnEnable()
    {
        try
        {
            InputSystem.EnableDevice(Gamepad.current);
            InputSystem.EnableDevice(Keyboard.current);
        }
        catch (Exception e)
        {
            Debug.LogError("No se ha encontrado el componente " + e);
        }
    }

    private void OnDisable()
    {
        InputSystem.DisableDevice(Gamepad.current);
        InputSystem.DisableDevice(Keyboard.current);
    }
    #endregion
    #region Script's logic
    private void Update()
    {
        Fire();
    }
    void Fire ()
    {
        if (shootPressed)
        {
            Debug.Log("Disparo");
            GameObject bala = Instantiate(projectile, transform.position, Quaternion.identity);

            bala.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);

            Destroy(bala, 2f);

            // Avoid multi-shots
            shootPressed = false;
        }

    }

    void PlayAudioAndSFX ()
    {
        if (shootPressed)
        {
            shootAudioSource.Play();
        }
        if (shootPressed)
        {
            
            shootParticle.Play();
        }

        else
        {
            Debug.Log("No audio/SFX found");
        }
    }

    //Creates the rapid fire shot --Poir finalizar
    public IEnumerator RapidFire() 
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(1 / fireRate);
        }
    }
    #region Input logic
    public void OnFire(InputAction.CallbackContext context)
    {
        // check if it's being done.
        if (context.performed)
        {
            Debug.Log("Btt shoot pressed");
            shootPressed = true;
            PlayAudioAndSFX();
        }
        else if (context.canceled)
        {
            // Puedes manejar algo espec�fico cuando se deja de presionar el bot�n
            //Hacer animacion de que no tenga balas
        }
    }
    #endregion
    #endregion
}
