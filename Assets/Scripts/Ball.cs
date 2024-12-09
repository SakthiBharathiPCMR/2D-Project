using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private float moveSpeed=5f;

    private Vector2 paddleOffset;
    private bool isBallMoved;

    private void Start()
    {
        paddleOffset = transform.position - paddle.transform.position;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isBallMoved = true;
        }

        if (isBallMoved)
        {
            transform.Translate(transform.up * Time.deltaTime * moveSpeed);
        }
    }


    private void LateUpdate()
    {
        if (isBallMoved) return;

        transform.position = paddle.transform.position + (Vector3)paddleOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
