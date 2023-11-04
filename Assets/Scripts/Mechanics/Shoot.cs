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
        InputSystem.EnableDevice(Gamepad.current);
        InputSystem.EnableDevice(Keyboard.current);
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

    //Creates the rapid fire shot
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
        // check if tit's being done.
        if (context.performed)
        {
            Debug.Log("Btt shoot pressed");
            shootPressed = true;
        }
        else if (context.canceled)
        {
            // Puedes manejar algo específico cuando se deja de presionar el botón
        }
    }
    #endregion
    #endregion
}
