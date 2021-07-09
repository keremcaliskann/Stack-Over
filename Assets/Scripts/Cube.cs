using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Collider boxCollider;
    public HingeJoint myHingeJoint;
    public Rigidbody connectedBody;
    GameController gameController;

    public bool connected;
    bool canCollide;
    public bool groundedCube;
    public bool groundedFloor;
    public bool groundedFoundation;

    private void Awake()
    {
        connected = true;

        gameController = FindObjectOfType<GameController>();
        myHingeJoint = GetComponent<HingeJoint>();
        connectedBody = GameObject.Find("Swing Point").GetComponent<Rigidbody>();

        //boxCollider = GetComponent<BoxCollider>();

        rigidBody = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        canCollide = true;
        //rigidBody.useGravity = false;
        //boxCollider.enabled = false;
        //rigidBody.isKinematic = true;
        rigidBody.maxDepenetrationVelocity = 1;

        myHingeJoint.autoConfigureConnectedAnchor = false;
        myHingeJoint.connectedBody = connectedBody;
        myHingeJoint.anchor = Vector3.zero;
        myHingeJoint.connectedAnchor = Vector3.zero;
        myHingeJoint.axis = new Vector3(0, 0, 1);
        myHingeJoint.enableCollision = false;
    }

    private void Update()
    {
        if (!gameController.settings && !gameController.gameOver)
        {
            if (connected)
            {
                canCollide = true;
                
            }
            if (transform.position.y < 0)
            {
                Destroy(gameObject, 1f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Foundation"))
        {
            if (groundedCube)
            {
                Destroy(gameObject, 1f);
            }
            groundedFoundation = true;
            rigidBody.isKinematic = true;
        }
        if (collision.transform.CompareTag("Cube")&&canCollide)
        {
            groundedCube = true;
            gameController.speed += 5f;
            Vector3 contactPoint = transform.position;
            Vector3 zeroPoint = collision.transform.position;
            float distanceX = Mathf.Abs(contactPoint.x - zeroPoint.x);

            if (distanceX < 0.75f)
            {
                var cubeRenderer = transform.GetComponent<Renderer>();
                cubeRenderer.material.SetColor("_Color", Color.red);
                var cubeRenderer2 = collision.transform.GetComponent<Renderer>();
                cubeRenderer2.material.SetColor("_Color", Color.green);
                if (distanceX <= 0.1f)
                {
                    gameController.combo++;
                    transform.position = new Vector3(zeroPoint.x, zeroPoint.y + 1f, zeroPoint.z);
                    transform.rotation = Quaternion.identity;
                    if (gameController.combo >= 3)
                    {
                        rigidBody.isKinematic = true;
                    }
                    gameController.score += 10 * gameController.combo;
                }
                else
                {
                    gameController.combo = 0;
                }
                if (distanceX > 0.1f && distanceX <= 0.2f)
                {
                    gameController.score += 7; 
                }
                if (distanceX > 0.2f && distanceX <= 0.4f)
                {
                    gameController.score += 4; 
                }
                if (distanceX > 0.4f && distanceX <= 0.6f)
                {
                    gameController.score += 2;
                }
                if (distanceX > 0.6f && distanceX <= 0.75f)
                {
                    gameController.score += 1;
                }
            }
            else
            {
                gameController.health--;
            }
        }
        if(collision.transform.CompareTag("Floor"))
        {
            groundedFloor = true;
            Destroy(gameObject, 1f);
        }
        canCollide = false;
    }
    private void OnDestroy()
    {
        gameController.cubes.Remove(this);
    }
}
