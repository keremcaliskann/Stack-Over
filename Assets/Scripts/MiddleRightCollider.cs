using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleRightCollider : MonoBehaviour
{
    public MiddleLeftCollider middeLeftCollider;
    public bool touched = false;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            touched = true;
            if (middeLeftCollider.touched)
            {
                middeLeftCollider.touched = false;
                touched = false;
                other.attachedRigidbody.AddForce(Vector3.right * 50);
            }
        }
    }
}
