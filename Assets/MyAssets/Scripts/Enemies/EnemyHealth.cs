using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float totalHealth = 100;
    public float currentHealth;
    public float damage = 20; //daño que quita cada vez que pasa el timer puesto

    public GameObject explosionPrefab;

    private void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);

        }
        Destroy(gameObject); //destruimos gobject enemigo
    }
}
