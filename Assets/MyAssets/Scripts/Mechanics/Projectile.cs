using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 0.0f;
    public Vector3 myDir;
    public float speed = 30.0f; //Probably don't need a slerp for this
    private Transform target;


    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void Move(Vector3 direction)
    {
        // Mover el proyectil en la dirección especificada
        transform.Translate(direction * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollisionTarget")
        {

        }
    }
}
