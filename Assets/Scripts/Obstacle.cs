using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;

    private GameObject gameControllerObject;
    private GameController gameController;

    public bool isEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // This is an enemy; lose health
        if (isEnemy)
        {
            if (collision.gameObject.tag == "Player0")
            {
                gameController.DecreaseHealth(0);
            }
            else if (collision.gameObject.tag == "Player1")
            {
                gameController.DecreaseHealth(1);
            }
        }
        // Not an enemy; gain points
        else
        {
            gameController.GainPoints();
        }

        if (collision.gameObject.tag != "Obstacle")
            Destroy(gameObject);
    }
}
