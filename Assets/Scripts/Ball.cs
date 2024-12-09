using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;

    [SerializeField] private Paddle paddle;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 paddleOffset;
    public static bool isBallMoved { get; private set; }

    private void Start()
    {
        GetPaddleOffset();
    }


    private void Update()
    {
        FireTheBall();

        MoveTheBall();

        DetectWall();


    }

    private void LateUpdate()
    {
        FollowPaddle();
    }

    private void MoveTheBall()
    {
        if (isBallMoved)
        {
            transform.Translate(transform.up * Time.deltaTime * moveSpeed);
        }
    }

    private void FireTheBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBallMoved = true;
        }
    }



    private void GetPaddleOffset()
    {
        paddleOffset = transform.position - paddle.transform.position;

    }

    private void FollowPaddle()
    {
        if (isBallMoved) return;

        transform.position = paddle.transform.position + (Vector3)paddleOffset;
    }


    public void RotatetheBall(Vector2 targetPos)
    {
        Vector2 rotateDirection = (targetPos - (Vector2)transform.position).normalized;

        float angleToRotate = (Mathf.Atan2(rotateDirection.y, rotateDirection.x)) * Mathf.Rad2Deg;
        Debug.Log(angleToRotate);



        transform.eulerAngles = new Vector3(0, 0, angleToRotate);

    }

    private void DetectWall()
    {
        if (transform.position.x > rightWall.position.x)
        {
            RotatetheBall(rightWall.position);
        }
        else if (transform.position.x < leftWall.position.x)
        {
            RotatetheBall(leftWall.position);
        }
        else if (transform.position.y > topWall.position.y)
        {
            RotatetheBall(topWall.position);
        }
        else if (transform.position.y < bottomWall.position.y)
        {
            RotatetheBall(bottomWall.position);
        }
    }



}
