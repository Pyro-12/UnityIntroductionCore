
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Inspector
    [Header("Max life")]
    [SerializeField] private float _maxHealth = 100f;
    [Header("life")] [Tooltip("Actual player's life")] [SerializeField] private float health = 100f;
    #endregion
    #region Auxiliary data
    
    private bool isDead;
    #endregion

    #region script's logic
    private void Start()
    {
        //health = _maxHealth;
    }
    //implement take damage
    void TakeDamage()
    {
        if (health < 0)
        {
            isDead = false;
        }

        else
        {
            isDead = true;
        }
    }
    #endregion
}
