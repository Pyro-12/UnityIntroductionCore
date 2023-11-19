
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Inspector
    [Header("Max life")]
    [SerializeField] private float _maxHealth = 100f;
    [Header("Life")] [Tooltip("Actual player's life")] [SerializeField] private float health = 100f;
    #endregion
    #region Auxiliary data
    
    private bool isDead;
    #endregion

    #region script's logic
    private void Start()
    {
        //health = _maxHealth;
    }

    private void Update()
    {
        //TakeDamage();

    }
    //implement take damage
   public  void TakeDamage(float damage)
    {
        Debug.Log("Player ha recibido daño: " + damage);
        health -= damage;
       
        
        if (health <= 0)
        {
            //isDead = true;
            PlayerDead();
        }

    }

    void PlayerDead ()
    {
        isDead = true;
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
                Debug.Log(health);
            }
            else
            {
                Debug.LogWarning("No se encontró el componente EnemyHealth en el objeto: " + other.name);
            }
        }
    }
    #endregion
}
