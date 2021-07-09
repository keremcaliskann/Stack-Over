using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    [Range(0, 50)]
    public float delay = 20;
    public Vector3 offset;
    Vector3 cameraPosition;
    Vector3 max;

    private void Start()
    {
        max = Vector3.zero;
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            cameraPosition = target.position + offset;
            Vector3 position = transform.position;
            position.y = Vector3.Lerp(transform.position, cameraPosition, 1 / delay).y;
            transform.position = position;
        }
    }
}
