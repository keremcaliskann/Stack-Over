using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleLeftCollider : MonoBehaviour
{
    public MiddleRightCollider middeRightCollider;
    public bool touched = false;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            touched = true;
            if (middeRightCollider.touched)
            {
                middeRightCollider.touched = false;
                touched = false;
                other.attachedRigidbody.AddForce(Vector3.left * 50);
            }
        }
    }
}
