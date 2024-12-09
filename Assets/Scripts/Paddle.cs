using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float xRange;
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, -xRange, xRange);
        transform.position = new Vector2(mousePos.x, transform.position.y);
    }
}
