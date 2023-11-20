using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject deathZoneTrigger;

    // Lista din�mica para almacenar los spawn points visitados
   [SerializeField] private List<GameObject> visitedSpawnpoints = new List<GameObject>();

    float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerStats != null)
        {
            Debug.Log("entro trigger");
            playerStats.TakeDamage(damage);

            // Obtener el spawn point actual
            GameObject currentSpawnpoint = GetClosestSpawnpoint(other.transform.position);

            // Asegurarse de que el spawn point no est� en la lista antes de agregarlo
            if (!visitedSpawnpoints.Contains(currentSpawnpoint))
            {
                visitedSpawnpoints.Add(currentSpawnpoint);
            }

            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("Respawning player...");

        if (visitedSpawnpoints.Count > 0)
        {
            // Obtener la posici�n del �ltimo spawn point visitado
            GameObject lastSpawnpoint = visitedSpawnpoints[visitedSpawnpoints.Count - 1];

            // Asignar la posici�n del �ltimo spawn point al jugador
            CharacterController characterController = playerStats.GetComponent<CharacterController>();

            if (characterController != null)
            {
                characterController.enabled = false;
                playerStats.transform.position = lastSpawnpoint.transform.position;
                characterController.enabled = true;

                // Restablecer la rotaci�n del jugador (puedes ajustar esto seg�n tus necesidades)
                playerStats.transform.rotation = Quaternion.identity;

                Debug.Log("Player respawned at: " + lastSpawnpoint.transform.position);
            }
            else
            {
                Debug.LogError("No se encontr� CharacterController en el jugador.");
            }
        }
        else
        {
            Debug.LogWarning("No hay spawnpoints visitados.");
        }
    }

    GameObject GetClosestSpawnpoint(Vector3 position)
    {
        // Encuentra el spawn point m�s cercano basado en alguna l�gica (puedes ajustar esto seg�n tus necesidades)
        // En este ejemplo, simplemente devuelve el primer spawn point que encuentre.
        GameObject closestSpawnpoint = null;

        foreach (var spawnpoint in visitedSpawnpoints)
        {
            if (closestSpawnpoint == null || Vector3.Distance(position, spawnpoint.transform.position) < Vector3.Distance(position, closestSpawnpoint.transform.position))
            {
                closestSpawnpoint = spawnpoint;
            }
        }

        return closestSpawnpoint;
    }
}
