using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBrick : MonoBehaviour
{


    public int brickHealth;

    public virtual void DamageBrick()
    {
        brickHealth--;
        if (brickHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.BALL_TAG))
        {
            DamageBrick();
        }

    }



}
