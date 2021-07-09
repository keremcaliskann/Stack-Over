using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollider : MonoBehaviour
{
    public SwingPoint swingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Swing Point"))
        {
            swingPoint.touchedRight = true;
            swingPoint.touchedLeft = false;
        }
    }
}
