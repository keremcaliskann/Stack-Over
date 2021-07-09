using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour
{
    public Transform fixedPoint;
    public Transform swingPoint;
    Rigidbody swingPointRb;

    public Cube2 cube;
    Cube2 newCube;

    public bool hasCube;

    public int rand;
    public int speed;

    private void Start()
    {
        speed = 100;
        swingPointRb = swingPoint.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!hasCube)
        {
            hasCube = true;
            newCube = null;
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                swingPointRb.AddForce(Vector3.right * -10000);
            }
            if (rand == 1)
            {
                swingPointRb.AddForce(Vector3.right * 10000);
            }
            Invoke("InstantiateCube", 0.1f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetTouch();
        }
        if (hasCube && newCube != null)
        {
            if (newCube.groundedCube || newCube.groundedFloor || newCube.groundedFoundation)
            {
                hasCube = false;
            }
        }
    }

    public void GetTouch()
    {
        if (hasCube)
        {
            Destroy(newCube.myHingeJoint);
            newCube.myRigidbody.velocity /= 5;
        }
    }

    void InstantiateCube()
    {
        newCube = Instantiate(cube, swingPoint.position, Quaternion.identity) as Cube2;
    }
}
