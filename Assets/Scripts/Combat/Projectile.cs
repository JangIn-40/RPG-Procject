using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 1f;

    void Update()
    {
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        return target.position + Vector3.up * targetCapsule.height / 2;
    }
}
