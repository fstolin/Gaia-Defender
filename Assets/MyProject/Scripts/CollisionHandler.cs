using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float f = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bump!");
        Debug.Log("You collided with:" + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("***You collided with:" + other.gameObject.name);
    }
}
