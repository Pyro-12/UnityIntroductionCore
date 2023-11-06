using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [Header("Stamina regenerate parametres")]
    [SerializeField][Range(0, 50)][Tooltip ("Stablish the amount of stamina lost")] float staminaDrain = 0.5f;
    [SerializeField][Range(0, 50)][Tooltip ("Stablish the amount of stamina regenerated")] float staminaRegen = 0.5f;

    [Space(10)] [Header("Stamina speed parametres")]
    private int slowedRun = 4;
    private int normalRun =8 ; //ver si tiene que ver con la velocidad normal del player controller

    [Space(10)]
    [Header("UGUI")]
    [SerializeField] private Image staminaImg;
    [SerializeField] private GameObject staminaPanel;

    [SerializeField] PlayerMovement player;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isSprinting)
        {
            if (player.Stamina <= player.MaxStamina)
            {
                player.Stamina += staminaRegen * Time.deltaTime;

                if (player.Stamina >= player.MaxStamina)
                {
                    staminaPanel.SetActive(true);
                    player.StaminaRegenerated = true;
                }
            }

            // Llamar a StaminaUpdate para actualizar la UI de la stamina
            StaminaUpdate();
        }

        void StaminaUpdate()
        {
            staminaImg.fillAmount = player.Stamina / player.MaxStamina;
        }

    }
}
