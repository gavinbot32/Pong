using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    public float moveSpeed;
    public float rotSpeed;
    public float maxRot;
    public Rigidbody2D rig;
    public float yInput;
    public float xInput;
    public float bound;
    public bool playerBool;
    public ControllerScript controller;
    public GameObject paddle1;
    public GameObject paddle2;
    public Vector2 paddle1vec;
    public Vector2 paddle2vec;
    public int playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Vector2 local = transform.localScale;
        paddle1vec = new Vector2(paddle1.transform.localScale.x, paddle1.transform.localScale.y);
        paddle2vec = new Vector2(paddle2.transform.localScale.x, paddle2.transform.localScale.y);
    }

    private void FixedUpdate()
    {
        if (playerBool)
        {
            yInput = Input.GetAxis("Vertical");
            xInput = Input.GetAxis("Horizontal2");
        }
        else
        {
            yInput = Input.GetAxis("VerticalTwo");
            xInput = Input.GetAxis("Horizontal1");
        }
        if (yInput != 0)
        {

            rig.velocity = new Vector2(0, yInput * moveSpeed);
            Debug.Log(yInput);
        }
        else
        {
            rig.velocity = new Vector2(0, 0);
            Debug.Log("no input");

        }
        if (controller.rotMode)
        {
            if (xInput != 0)
            {
                rig.AddTorque(xInput * rotSpeed);
                if (rig.angularVelocity > maxRot)
                {
                    rig.angularVelocity = maxRot;
                }
                else if (rig.angularVelocity < -maxRot)
                {
                    rig.angularVelocity = -maxRot;
                }
            }
            else
            {
                rig.angularVelocity = 0f;
            }

        }
    }


        // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= bound)
        {
            transform.position = new Vector3(transform.position.x, bound, transform.position.z);
        }
        if (transform.position.y <= -bound)
        {
            transform.position = new Vector3(transform.position.x, -bound, transform.position.z);
        }
    }

    public void changeSize(int playerIn)
    {
        if (controller.sizeMode)
        {
            if (playerIn == playerIndex)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y - 0.09f);
            }
            else
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + 0.09f);
            }
        }
    }
    
}
