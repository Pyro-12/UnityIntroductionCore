using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField] GameObject deathZoneTrigger;
    [SerializeField] GameObject[] spawnpoint;
    float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entro trigger");
            playerStats.TakeDamage(damage);
            Respawn();
        }
    }

   void  Respawn()
   {
        //get spawn point
   }
}
