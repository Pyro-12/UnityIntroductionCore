using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlelr : MonoBehaviour
{
    Transform player;
    AIController aiController;
    float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aiController.GetComponent<AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChasePlayer()
    {
        //aiController.PlayerPersecuted();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Me han dado");
            Destroy(gameObject, 2f);

        }
    }
}
