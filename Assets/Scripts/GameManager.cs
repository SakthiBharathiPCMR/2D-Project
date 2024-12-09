using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        BallInPaddle,
        BallFired,
    }

    public static GameState gameState { get; private set; }

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }


    private void Start()
    {
        gameState = GameState.BallInPaddle;
    }

    public static void UpdateGameState()
    {
        gameState++;
    }

}
