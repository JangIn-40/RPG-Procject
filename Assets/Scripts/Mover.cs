using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;


    
    void Update()
    {

        UpdateAnimator();
    }


    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelcotiy = transform.InverseTransformDirection(velocity);
        float speed = localVelcotiy.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
