using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Cube> cubes;
    public Cube cube;
    Cube newCube;

    public GameObject fixedPoint;
    public SwingPoint swingPoint;
    public GameObject colliders;

    public bool hasObject;
    public bool canInstantiate;
    Vector3 peak;

    public int score;
    public int combo;
    public Text scoreText;
    public Text comboText;

    public GameObject touchableArea;
    public GameObject buttonsGo;
    public GameObject settingsGo;
    public GameObject gameOverGo;
    public bool settings;
    public bool gameOver;
    public bool touched;

    public int health;
    public float speed;

    private void Awake()
    {
        touchableArea.SetActive(true);
        settingsGo.SetActive(false);
        gameOverGo.SetActive(false);
        buttonsGo.SetActive(true);
    }

    private void Start()
    {
        speed = 100;
        score = 0;
        combo = 0;
        health = 3;
    }

    void Update()
    {
        if (health <= 0)
        {
            GameOver();
        }
        if (!settings && !gameOver)
        {
            if (cube != null)
            {
                peak = GetHeighestPoint();
                scoreText.text = score.ToString();
                if (combo > 0)
                {
                    comboText.gameObject.SetActive(true);
                    comboText.text = "x" + combo.ToString();
                }
                else
                {
                    comboText.gameObject.SetActive(false);
                }

                if (canInstantiate)
                {
                    canInstantiate = false;
                    newCube = Instantiate(cube, swingPoint.transform.position, Quaternion.identity) as Cube;
                    cubes.Add(newCube);
                    newCube.connected = true;
                    hasObject = true;
                    newCube.rigidBody.isKinematic = false;
                    if (swingPoint.touchedLeft)
                    {
                        newCube.rigidBody.AddForce(Vector3.left * speed);
                    }
                    if (swingPoint.touchedRight)
                    {
                        newCube.rigidBody.AddForce(Vector3.right * speed);
                    }
                }
                
                if (newCube != null)
                {
                    if (newCube.groundedCube || newCube.groundedFloor || newCube.groundedFoundation)
                    {
                        hasObject = false;
                        touched = false;
                    }
                }
                Raise();
            }
        }
    }

    public void Raise()
    {
        float time = 0;
        while (time < 3f)
        {
            Vector3 hookTargetPosition = new Vector3(0, peak.y + 6, 0);
            fixedPoint.transform.position = Vector3.Lerp(fixedPoint.transform.position, hookTargetPosition, Time.deltaTime);
            colliders.transform.position= Vector3.Lerp(colliders.transform.position, hookTargetPosition, Time.deltaTime);

            time += Time.deltaTime;
        }
    }

    Vector3 GetHeighestPoint()
    {
        Vector3 heighestPoint = new Vector3(0, 1, 0);
        if (cubes != null)
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                if (cubes[i].groundedCube)
                {
                    if (cubes[i].transform.position.y > heighestPoint.y)
                    {
                        heighestPoint = cubes[i].transform.position;
                    }
                }
            }
        }
        return heighestPoint;
    }

    public void Pause()
    {
        settings = true;
        scoreText.gameObject.SetActive(false);
        settingsGo.SetActive(true);
        newCube.rigidBody.isKinematic = true;
        touchableArea.SetActive(false);
    }
    public void Resume()
    {
        settings = false;
        scoreText.gameObject.SetActive(true);
        settingsGo.SetActive(false);
        newCube.rigidBody.isKinematic = false;
        touchableArea.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GetTouch()
    {
        if (hasObject && !touched)
        {
            touched = true;
            newCube.connected = false;
            //newCube.rigidBody.isKinematic = false;
            //newCube.rigidBody.useGravity = true;
            //newCube.boxCollider.enabled = true;
            Destroy(newCube.myHingeJoint);
            newCube.rigidBody.velocity /= 5;
            AddForceSwingPoint();
        }
    }

    void AddForceSwingPoint()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            swingPoint.rigidBody.AddForce(Vector3.right * 5000);
        }
        if (rand == 1)
        {
            swingPoint.rigidBody.AddForce(Vector3.left * 5000);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverGo.SetActive(true);
        touchableArea.SetActive(false);
        buttonsGo.SetActive(false);
        comboText.gameObject.SetActive(false);
    }
}
