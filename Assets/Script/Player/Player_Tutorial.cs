using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tutorial : PlayerBase
{
    bool isRescue = false;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isRescue == true)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(0, 5f);
            if(transform.position.y >= -1)
            {
                rigidBody.gravityScale = gravityPower;
                isRescue = false;
                animator.SetBool("RescueDown", true);
            }
        }        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LeftButtonAction(string key)
    {
        base.LeftButtonAction(key);
    }

    public override void RightButtonAction(string key)
    {
        base.RightButtonAction(key);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") == true)
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Obstacle") == true && ableObstacleHit == true)
        {
            obstacleHit = true;
            ableObstacleHit = false;            
            StartCoroutine(ObstacleCrash());
        }

        if (collision.gameObject.CompareTag("DropZone") == true)
        {
            obstacleHit = true;
            isRescue = true;
            animator.SetTrigger("RescueUp");
        }
    }
}
