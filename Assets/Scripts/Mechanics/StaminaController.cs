using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [Header("Stamina regenerate parameters")]
    [SerializeField] [Range(0, 50)] [Tooltip("Stablish the amount of stamina lost")] private float staminaDrain = 0.5f;
    [SerializeField] [Range(0, 50)] [Tooltip("Stablish the amount of stamina regenerated")] private float staminaRegen = 0.5f;

    [Space(10)]
    [Header("Stamina speed parameters")]
    private int slowedRun = 4;
    /*private int normalRun = 8; //ver si tiene que ver con la velocidad normal del player controller*/

    [Space(10)]
    [Header("UGUI")]
    [SerializeField] private Image staminaImg;
    [SerializeField] private GameObject staminaPanel;

    private PlayerMovement playerMovement;
    private Coroutine regenerationCoroutine;
     bool isRegenerating = true;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();        
    }
    void Update()
    {
        SprintTimer();
    }

    void SprintTimer()
    {
        if (playerMovement.IsSprinting)
        {
            DrainStamina();
        }
        else 
        {
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

    //Add a coroutine to cooldown stamina bar before regeneration
    IEnumerator RegenerateStaminaAfterDelay()
    {
        if (playerMovement.Stamina <= 0) yield return new WaitForSeconds(1f);
       
            RegenerateStamina();
      
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

    void StaminaUpdate()
    {
        staminaImg.fillAmount = playerMovement.Stamina / playerMovement.MaxStamina;
    }

    void ActivateStaminaPanel()
    {
        staminaPanel.SetActive(true);
    }

}
