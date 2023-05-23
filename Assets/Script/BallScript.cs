using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public float speed = 1;
    public Rigidbody2D rig;

    public float minXSpeed = 1.8f;
    public float maxXSpeed = 3f;

    public float minYSpeed = 1.2f;
    public float maxYSpeed = 2f;

    public ControllerScript controller;
    public PaddleScript paddle1;
    public PaddleScript paddle2;
    public float difMultiplier = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        paddle1 = GameObject.FindGameObjectWithTag("paddle1").GetComponent<PaddleScript>();
        paddle2 = GameObject.FindGameObjectWithTag("paddle2").GetComponent<PaddleScript>();
        controller = GameObject.FindObjectOfType<ControllerScript>();
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(
            Random.Range(minXSpeed, maxXSpeed) * (Random.value > 0.5f ? -1 : 1), 
            Random.Range(minXSpeed, maxXSpeed) * (Random.value > 0.5f ? -1 : 1));
    }


    private void Update()
    {
        checkScore();
    }
    public void checkScore()
    {
        if (transform.position.x >= 3)
        {
            controller.playerScore(true);
            Destroy(gameObject);
            
        }
        if (transform.position.x <= -3)
        {
            controller.playerScore(false);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bounds"))
        {
            if (collision.transform.position.y > transform.position.y && rig.velocity.y > 0)
            {
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * -1);
            }

            if (collision.transform.position.y < transform.position.y && rig.velocity.y < 0)
            {
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * -1);
            }


        }
        else if (collision.CompareTag("paddle1")|| collision.CompareTag("paddle2"))
        {
            float yMult = 1;
            if (collision.CompareTag("paddle1"))
            {
                yMult = paddle1.yInput;
            }
            else
            {
                yMult = paddle2.yInput;
            }
            if (yMult < 1 && yMult > -1)
            {
                yMult = 1;
            }
            Mathf.Abs(yMult);
            if (collision.transform.position.x < transform.position.x && rig.velocity.x < 0)
            {
                rig.velocity = new Vector2(-rig.velocity.x * difMultiplier
                    , rig.velocity.y*yMult);
            }
            else
            {
                rig.velocity = new Vector2(-rig.velocity.x * difMultiplier,
                    rig.velocity.y*yMult);
            }



        }
    }


}