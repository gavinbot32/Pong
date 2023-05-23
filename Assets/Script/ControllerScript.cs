using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ControllerScript : MonoBehaviour
{
    public GameObject ballPrefab;
    private BallScript newBall;
    public PaddleScript[] paddles;
    public GameObject chaosPackage;

    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;

    public int score1;
    public int score2;
    public bool sizeMode;
    public bool rotMode;
    public bool chaosBool;
    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
        chaosBool = false;
        chaosPackage.active = false;
        scoreText1 = GameObject.FindGameObjectWithTag("Score1").GetComponent<TextMeshProUGUI>();
        scoreText2 = GameObject.FindGameObjectWithTag("Score2").GetComponent<TextMeshProUGUI>();

        textUpdate();
        spawnBall();

    }
    private void textUpdate()
    {
        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();

    }
    private void spawnBall()
    {
        gameObject.transform.position = Vector3.zero;
        GameObject curBall = Instantiate(ballPrefab, transform);
        newBall = curBall.GetComponent<BallScript>();
        newBall.transform.position = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if ((score1 >= 11) && (score1 - score2 >= 2))
        {
            SceneManager.LoadScene("win1");
            
        }
        if ((score2 >= 11) && (score2 - score1 >= 2))
        {
            SceneManager.LoadScene("win2");
        }
    }
    public void playerScore(bool playerBool)
    {
        if (playerBool)
        {
            score1++;
            foreach (PaddleScript paddle in paddles) {

                paddle.changeSize(1);
            }

            
        }
        else
        {
            score2++;
            foreach (PaddleScript paddle in paddles)
            {

                paddle.changeSize(2);
            }
        }
        textUpdate();
        spawnBall();
    }
    public void rotModeSwitch()
    {
        foreach (PaddleScript paddle in paddles)
        {
            paddle.rig.angularVelocity = 0f;
            paddle.gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }

        rotMode = !rotMode;
        
    }
    public void sizeModeSwitch()
    {
        sizeMode = !sizeMode;
        foreach (PaddleScript paddle in paddles)
        {
            paddle.gameObject.transform.localScale = new Vector2(1f, 1f);
        }
    }
    public void normMode()
    {
        
        sizeMode = false;
        rotMode = false;
        foreach(PaddleScript paddle in paddles)
        {
            paddle.rig.angularVelocity = 0f;
            paddle.gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f,0f);
            paddle.gameObject.transform.localScale = new Vector2(1f, 1f);
        }
        
    }
    public void chaosMode()
    {
        chaosBool = !chaosBool;
        chaosPackage.active = chaosBool;
    }
}
