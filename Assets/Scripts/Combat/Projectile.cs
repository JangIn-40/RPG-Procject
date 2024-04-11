using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] bool isHoming = true;
    [SerializeField] GameObject hitEffect = null;

    Health target;
    float damage = 0;

    void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if(target == null) return;

        if(isHoming && !target.IsDie())
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null) 
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Health>() != target) return;
        if(target.IsDie()) return;
        target.TakeDamage(damage);

        if(hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }

        Destroy(gameObject);
    }
}
