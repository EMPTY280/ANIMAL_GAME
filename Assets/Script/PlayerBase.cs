using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBase : MonoBehaviour
{
    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected Rope rope;

    protected LayerMask groundLayer;
    protected LayerMask ropeLayer;
    protected LayerMask ropeEndLayer;

    [SerializeField] protected float jumpPower = 15.0f;
    protected float gravityPower = 5f;
    protected int maxJump = 2;
    [SerializeField] protected int ableJump;
    [SerializeField] protected bool onGround = false;
    protected bool onRope = false;
    [SerializeField] protected bool ableRopeAction = false;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        ropeLayer = 1 << LayerMask.NameToLayer("Rope");
        ropeEndLayer = 1 << LayerMask.NameToLayer("RopeEnd");
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidBody.gravityScale = gravityPower;
    }

    protected virtual void FixedUpdate()
    {
        RopeCheck();
        GroundCheck();
    }

    public virtual void LeftButtonAction(string key)
    {
        switch (key)
        {
            case "Down":
                if (ableJump > 0)
                {
                    Jump();
                }
                break;

            case "Stay":
                break;

            case "Up":
                break;
        }
    }

    public virtual void RightButtonAction(string key)
    {
        switch (key)
        {
            case "Down":
                if (ableRopeAction == true)
                {
                    Rope(true);
                }
                break;

            case "Stay":
                if (onGround == true)
                {
                    Slide(true);
                }
                break;

            case "Up":
                if (onGround == true)
                {
                    Slide(false);
                }
                else if (onRope == true)
                {
                    Rope(false);
                }
                break;
        }
    }

    protected void Jump()
    {
        Slide(false);
        rigidBody.velocity = new Vector2(0f, jumpPower);
        animator.SetBool("OnJump", true);
        onGround = false;
        ableJump--;
    }

    protected void Slide(bool onSlide)
    {
        float xTemp = transform.localScale.x;
        float yTemp = transform.localScale.y;

        if (onSlide == true)
        {
            boxCollider.size = new Vector2(xTemp, yTemp / 2);
            boxCollider.offset = new Vector2(0, yTemp / 4);
        }
        else
        {
            boxCollider.size = new Vector2(xTemp, yTemp);
            boxCollider.offset = new Vector2(0, yTemp / 2);
        }

        animator.SetBool("OnSlide", onSlide);
    }

    protected void Rope(bool _onRope)
    {
        if (_onRope == true)
        {
            rigidBody.gravityScale = 0f;
            rigidBody.velocity = Vector2.zero;
            rope.CatchRope(transform.position.x + 0.6f);
            onRope = true;
            ableJump = 0;
        }
        else
        {
            rigidBody.gravityScale = gravityPower;
            rigidBody.velocity = new Vector2(0f, jumpPower);
            rope.MissRope();
            onRope = false;
        }

        animator.SetBool("OnRope", _onRope);
    }

    protected void LandingSet()
    {
        onGround = true;
        ableJump = maxJump;
        animator.SetBool("OnJump", false);
    }

    protected void GroundCheck()
    {
        if (rigidBody.velocity.y < 0 && Physics2D.Raycast(transform.position, Vector2.down, 0.3f, groundLayer))
        {
            LandingSet();
        }
    }

    protected void RopeCheck()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector2.right, 1f, ropeLayer);

        if (raycastHit.collider != null)
        {
            ableRopeAction = true;
            rope = raycastHit.collider.gameObject.GetComponent<Rope>();
        }
        else
        {
            ableRopeAction = false;
        }

        if(Physics2D.Raycast(transform.position, Vector2.right, 1f,ropeEndLayer) && onRope == true)
        {
            Rope(false);
        }
    }
}
