using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    

    void LateUpdate()
    {
        transform.position = target.position;
    }
}
