using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTAction : MonoBehaviour
{
    public Animator Mov;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private const float SPEED_JUMP = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Mov = GetComponent<Animator>();
    }

    void Update()
    {
        float dist = 5.0f * Time.deltaTime;
        Vector2 pos = transform.position;

        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(3, 3, 1);
            Mov.ResetTrigger("Attack1");
            Mov.SetTrigger("Run");
            pos.x += dist;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-3, 3, 1);
            Mov.ResetTrigger("Attack1");
            Mov.SetTrigger("Run");
            pos.x -= dist;
        }

        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            Mov.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, SPEED_JUMP);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
