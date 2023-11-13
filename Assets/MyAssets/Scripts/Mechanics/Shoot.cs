using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputController))]
public class Shoot : MonoBehaviour
{
    #region Data
    [Header("Shoot")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform launchPoint;
    [SerializeField] float projectileSpeed = 30f;
    [SerializeField][Tooltip ("Cooldown time between shots")] float cooldownTime = 3f;
    [SerializeField] bool shootPressed;

    [Space(10)] [Header("Weapon stats")]
    [SerializeField] float weaponDamage = 10f;
    [SerializeField] float range = 50;
    [SerializeField] float fireRate = 100f;
    [SerializeField] int currentAmmo = 10;
    [SerializeField] int maxAmmo = 10;

    [Space(10)][Header("VFX")]
    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private AudioSource shootAudioSource;
    #endregion
    #region Auxiliary data
    // AUXILIARY DATA
    private bool canShoot = true;
    [SerializeField] bool isReloading;
    private PlayerInputController playerInputController;

    #endregion
    #region Initialize Script

    private void Start()
    {
        // Obtener referencia al PlayerInputController
        playerInputController = GetComponentInParent<PlayerInputController>();
        // Suscribirse al evento OnShoot
        playerInputController.OnShoot += OnShoot;
 
    }

    #endregion
    #region Script's logic
    private void ShootProjectile()
    {
        // Lógica para instanciar el proyectil
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);


        // Lanzar un Raycast desde la posición de lanzamiento en la dirección hacia adelante
        if (projectile != null)
        {
            // Obtener el componente Rigidbody del proyectil
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

            if (projectileRigidbody != null)
            {
                // Aplicar velocidad al proyectil
                projectileRigidbody.velocity = launchPoint.forward * projectileSpeed;
            }

            // Lanzar un Raycast desde la posición de lanzamiento en la dirección hacia adelante
            Ray ray = new Ray(launchPoint.position, launchPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                // Verificar si el objeto impactado tiene el componente EnemyHealth
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                // Si tiene el componente, aplicarle daño
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(weaponDamage);
                }

                // También podrías realizar otras acciones aquí, como efectos visuales o sonidos
            }
            //Destroy after shoot
            Destroy(projectile, 2.0f);
        }
    }

    //coroutine for waiting in-between shots
    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    private void ResetShootCooldown()
    {
        canShoot = false;
        StartCoroutine(CooldownCoroutine());
    }

    public void Reload()
    {
        if (isReloading)
        {
            Debug.Log("Reload");
            isReloading = true;
            // invoke? espera?
        }
    }
    #region SFX & VFX
    void PlayAudioAndSFX()
    {
        shootAudioSource.Play();
        shootParticle.Play();
    }
    #endregion
    #endregion
    #region Input System Relation
    private void OnShoot(InputAction.CallbackContext context)
    {
        if (canShoot)
        {
            Debug.Log("Disparando");
            ShootProjectile();
            PlayAudioAndSFX();
            //Re-start condition for coroutine
            ResetShootCooldown();
        }
        else
        {
            Debug.Log("No se puede disparar en este momento");
        }
    }
    #endregion

}


