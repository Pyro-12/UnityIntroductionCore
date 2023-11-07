using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    #region Data
    [Header("Stamina regenerate parameters")]
    [SerializeField] [Range(0, 50)] [Tooltip("Stablish the amount of stamina lost")] private float staminaDrain = 0.5f;
    [SerializeField] [Range(0, 50)] [Tooltip("Stablish the amount of stamina regenerated")] private float staminaRegen = 0.5f;

    [Space(10)]
    [Header("Stamina speed parameters")]
    [Tooltip("Stablish the exhausted run after sprint")] private int slowedRun = 4; //ver si se puede relacionar con speed de playermovement
    /*private int normalRun = 8; //ver si tiene que ver con la velocidad normal del player controller*/

    [Space(10)]
    [Header("UGUI")]
    [SerializeField] [Tooltip("Indicates the stamina left")] private Image staminaImg;
    [SerializeField] [Tooltip("Background of the image")] private GameObject staminaPanel;
    #endregion
    #region Auxiliary data
    //AUXILIARY DATA
    private PlayerMovement playerMovement;
    private Coroutine regenerationCoroutine;
    bool isRegenerating = true;
    #endregion
    #region Init Script
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();        
    }
    #endregion
    #region Script logic
    void Update()
    {
        SprintTimer();
    }

    void SprintTimer()
    {
        //check is player is sprinting and create method for drain/regenerate stamina bar
        if (playerMovement.IsSprinting)
        {
            DrainStamina();
        }
        else 
        {
            //if stamina = 0 stop de bar for a cooldown
            StartCoroutine(RegenerateStaminaAfterDelay());
        }
    }
    void DrainStamina()
    {
        if (playerMovement.Stamina > 0)
        {
            isRegenerating = false;
            playerMovement.Stamina -= staminaDrain * Time.deltaTime;
            StaminaUpdate();
        }
            StaminaUpdate();
    }
    
    void RegenerateStamina()
    {
        if (playerMovement.Stamina < playerMovement.MaxStamina)
        {
            playerMovement.Stamina += staminaRegen * Time.deltaTime;
            StaminaUpdate();
        }
        else
        {
            playerMovement.Stamina = playerMovement.MaxStamina;
            StaminaUpdate();
            playerMovement.StaminaRegenerated = true;
        }
    }
    //Add a coroutine to cooldown stamina bar before regeneration
    IEnumerator RegenerateStaminaAfterDelay()
    {
        if (playerMovement.Stamina <= 0) yield return new WaitForSeconds(1f);
        RegenerateStamina();
    }
    void StaminaUpdate()
    { 
        //Translate the stamina into visual
        staminaImg.fillAmount = playerMovement.Stamina / playerMovement.MaxStamina;
    }
    #endregion
}
