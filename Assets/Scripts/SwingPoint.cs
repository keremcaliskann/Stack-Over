using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingPoint : MonoBehaviour
{
    public Rigidbody rigidBody;
    GameController gameController;
    HingeJoint myHingeJoint;
    public bool touchedRight;
    public bool touchedLeft;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<GameController>();
        myHingeJoint = GetComponent<HingeJoint>();
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            rigidBody.AddForce(Vector3.right * 5000);
        }
        if (rand == 1)
        {
            rigidBody.AddForce(Vector3.left * 5000);
        }
    }
    private void Update()
    {
        if (!gameController.hasObject && (touchedLeft || touchedRight))
        {
            gameController.canInstantiate = true;
            gameController.touched = false;
            touchedLeft = false;
            touchedRight = false;
        }
    }
}
