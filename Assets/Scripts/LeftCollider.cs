using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCollider : MonoBehaviour
{
    public SwingPoint swingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Swing Point"))
        {
            swingPoint.touchedLeft = true;
            swingPoint.touchedRight = false;
        }
    }
}
