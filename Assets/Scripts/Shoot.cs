using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    #region
    [Header ("Shoot")]
    [SerializeField] GameObject projectile;
    [SerializeField] float speed;
    [SerializeField] bool shootPressed;
    [Space(10)][Header("Weapon stats")]
    [SerializeField] float range = 50;
    [SerializeField] float damage = 10f;
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

            // Reinicia el estado de shootPressed para evitar múltiples disparos en una sola pulsación
            shootPressed = false;
        }

        else
        {
            shootPressed = false;
        }

    }

    void OnFire(InputValue value)
    {
        Debug.Log("Btt shoot pressed");
        shootPressed = value.isPressed;
    }
    #endregion
}
