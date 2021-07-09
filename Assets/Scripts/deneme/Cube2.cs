using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{
    GameController2 gameController;

    public Rigidbody myRigidbody;
    public HingeJoint myHingeJoint;
    public Rigidbody connectedBody;

    public bool groundedCube;
    public bool groundedFloor;
    public bool groundedFoundation;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController2>();

        myHingeJoint = gameObject.AddComponent<HingeJoint>();
        connectedBody = GameObject.Find("Swing Point").GetComponent<Rigidbody>();

        myRigidbody = GetComponent<Rigidbody>();

        myHingeJoint.autoConfigureConnectedAnchor = false;
        myHingeJoint.connectedBody = connectedBody;
        myHingeJoint.anchor = Vector3.zero;
        myHingeJoint.connectedAnchor = Vector3.zero;
        myHingeJoint.axis = new Vector3(0, 0, 1);
        myHingeJoint.enableCollision = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        groundedCube = true;
        groundedFloor = true;
        groundedFoundation = true;
        gameController.speed += 50;
    }
}
