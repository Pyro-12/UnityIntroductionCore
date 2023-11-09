using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent enemy;
    // Start is called before the first frame update
    void Start()
    {
        //destination = GameObject.FindGameObjectWithTag("Destination");
        enemy = GetComponent<NavMeshAgent>();

      //  agent.SetDestination(destination.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = player.position;
    }
}
