using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    #region Data Script
    [Header ("Navmesh data")]
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] [Tooltip("Area for focus in player follow")] float radius;

    Transform currentTarget;
    Animator animator;
    bool isChasingPlayer;

    #endregion

    #region Auxiliary data
    //AUXILIARY DATA
    bool playerIsNear;
    #endregion

    #region Initialize scritp
    // Start is called before the first frame update
    void Start()
    {
        //destination = GameObject.FindGameObjectWithTag("Destination");
        enemy = GetComponent<NavMeshAgent>();
        if (enemy == null)
        {
            Debug.LogError("El componente NavMeshAgent no se encontró en el objeto " + gameObject.name);
            // Puedes manejar este error de la manera que prefieras.
            // Por ejemplo, podrías deshabilitar este script o destruir el objeto.
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        //agent.SetDestination(destination.transform.position);
    }
    #endregion
    #region Logic Srcipt
    // Update is called once per frame
    void Update()
    {
        ChasingPlayer();
       
    }

    void ChasingPlayer ()
    {
        playerIsNear = false;
        int playerLayer = LayerMask.GetMask("Character");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, playerLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                currentTarget = player;  // Cambiar al jugador como objetivo
                enemy.destination = currentTarget.position;
            }
        }
      //  ChangeAnimationState(); // TO-DO animacion enemigo
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Me han dado");
            Destroy(gameObject, 2f);

        }
    }

    void ChangeAnimationState()
    {
        animator.SetBool("ChasePlayer", isChasingPlayer);
    }
    #endregion
}
