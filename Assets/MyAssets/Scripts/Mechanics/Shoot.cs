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
    [SerializeField] float projectileForce;
    [SerializeField] float cooldownTime = 3f;
    [SerializeField] bool shootPressed;
    [Space(10)]
    [Header("Weapon stats")]
    [SerializeField] float range = 50;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentAmmo = 10;
    [SerializeField] float fireRate = 100f;
    [Header("VFX")]
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
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
    }

    private void ResetShootCooldown()
    {
        canShoot = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
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

    void PlayAudioAndSFX()
    {
        shootAudioSource.Play();
        shootParticle.Play();
    }
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


