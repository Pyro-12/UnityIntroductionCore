using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region Data
    [SerializeField] PlayerStats playerStats;
    [Space(10)] [Header("UI Stats")]
    [SerializeField] Image _lifeImg;
    #endregion

    #region Initialize Script
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        // Suscribirse al evento OnHealthChanged
        playerStats.OnHealthChanged += UpdateLifeBar;
    }
    private void OnDestroy()
    {
        playerStats.OnHealthChanged -= UpdateLifeBar;
    }

    #endregion


    void UpdateLifeBar(float health)
    {
 
            _lifeImg.fillAmount = health / playerStats.GetMaxHealth();
        

            Debug.LogWarning("actualizo");
        
    }

    void HandlePlayerDeath()
    {
        // TO-DO actualize death logic here
    }
}
