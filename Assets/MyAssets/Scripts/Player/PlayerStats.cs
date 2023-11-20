using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Inspector
    [Header("Max life")]
    [SerializeField]  float _maxHealth = 100f;
    [Header("Life")] [Tooltip("Actual player's life")] [SerializeField]  float _health = 100f;
    #endregion
    #region Auxiliary data
    
    private bool isDead;
    #endregion
    #region Events
    public event Action OnPlayerDeath;
    public event Action<float> OnHealthChanged;
    #endregion
    public float GetMaxHealth()
    {
        return _maxHealth;
    }
    private void Start()
    {
        //health = _maxHealth;
    }

    #region script's logic

    private void Update()
    {
        //TakeDamage();

    }
    //implement take damage
   public  void TakeDamage(float damage)
    {
        Debug.Log("Player ha recibido daño: " + damage);
        _health -= damage;
        OnHealthChanged?.Invoke(_health);
        // Asegurarse de que la vida no sea menor que 0
        _health = Mathf.Max(0f, _health);

        if (_health <= 0)
        {
            //isDead = true;
            PlayerDead();
        }

    }


    void PlayerDead()
    {
        isDead = true;
        OnPlayerDeath?.Invoke();
    //animator.SetTrigger("Dead"); //TO-DO animation dead for 3person
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy detected.");

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                Debug.Log("EnemyHealth component found. Taking damage.");
                TakeDamage(enemyHealth.damage);
                Debug.Log(_health);
            }
            else
            {
                Debug.LogWarning("No se encontró el componente EnemyHealth en el objeto: " + other.name);
            }
        }
    }
    #endregion
}
