using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float distToGround;

    private float horizontal_multiplier = 6f;
    private float vertical_multiplier = 8f;
    private float fallMultiplier = 8f;
    private float resistance = 0f;

    private enum State { active, inactive };
    State state;

    public bool isTopPlayer;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<BoxCollider>().bounds.extents.y;
        state = State.active;
    }

    private void Update()
    {
        MovePlayer();
        Jump();
        ApplyResistance();

        if (state == State.inactive)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyResistance()
    {
        rb.velocity = new Vector3(rb.velocity.x - resistance, rb.velocity.y, 0);
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * horizontal_multiplier;
        rb.velocity = new Vector3(horizontal, rb.velocity.y, 0);
    }

    private void Jump()
    {
        // Fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += new Vector3(0, 1, 0) * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
        {
            rb.velocity = new Vector3(0, 1, 0) * vertical_multiplier;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public void KillPlayer()
    {
        state = State.inactive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            if (isTopPlayer)
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().InstKillPlayer(0);
            else
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().InstKillPlayer(1);
        }
    }
}
