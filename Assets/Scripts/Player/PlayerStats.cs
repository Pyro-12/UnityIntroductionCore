
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Inspector
    [Header("Max life")]
    [SerializeField] private float _maxHealth = 100f;
    #endregion
    #region Auxiliary data
    [Header("life")][Tooltip("Actual player's life")][SerializeField] private float health = 100f;
    #endregion

}
