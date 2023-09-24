using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rigidbody2d = null;
    private SpriteRenderer sprite = null;

    [SerializeField] private float jumpPower = 0.0f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool jump = false;

    private float hitCounter;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (!rigidbody2d || !sprite)
            this.enabled = false;
    }

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.0f, ground))
            isGrounded = true;
        else
            isGrounded = false;

        if (isGrounded && (jump || Input.GetButton("Jump")))
        {
            jump = false;
            rigidbody2d.velocity = new Vector2(0f, jumpPower);
        }
    }
 
    private void FixedUpdate()
    {
        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
            if (hitCounter <= 0)
            {
                RestoreHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle")
            Hit();
    }

    public void Hit()
    {
        sprite.color = Color.red;
        hitCounter = 0.5f;
    }

    private void RestoreHit()
    {
        sprite.color = Color.white;
    }
}
