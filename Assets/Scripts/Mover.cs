using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;


    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }


    void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        bool hasHit = Physics.Raycast(ray, out Hit);
        if(hasHit)
        {
            GetComponent<NavMeshAgent>().destination = Hit.point;
        }
    }

    void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelcotiy = transform.InverseTransformDirection(velocity);
        float speed = localVelcotiy.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
