using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 paddleOffset;
    public static bool isBallMoved { get; private set; }

    private void Start()
    {
        GetPaddleOffset();
        transform.parent = paddle.transform;
    }

    private void GetPaddleOffset()
    {
        paddleOffset = paddle.transform.position - transform.position;
    }


    private void Update()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.BallInPaddle:
                CheckForInput();
                break;

            case GameManager.GameState.BallFired:
                MoveTheBall();
                break;
        }

    }

    private void MoveTheBall()
    {
        if (isBallMoved)
        {
            transform.position += transform.up * Time.deltaTime * moveSpeed;
        }
    }

    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBallMoved = true;
            transform.parent = null;
            GameManager.UpdateGameState();
            transform.eulerAngles = Vector3.forward * 50;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBallMoved) return;

        Vector2 inNormal = collision.GetContact(0).normal;
        Debug.Log("Innormal"+inNormal);
        Vector2 reflectionDirection = Vector3.Reflect(transform.up, inNormal);

        Debug.Log("Ref"+reflectionDirection);

        transform.up = reflectionDirection;

    }




}
