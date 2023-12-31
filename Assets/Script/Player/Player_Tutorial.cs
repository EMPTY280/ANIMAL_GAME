using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tutorial : PlayerBase
{
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
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("DropZone") == true)
        {
            isRescue = true;
            animator.SetTrigger("RescueUp");
        }
    }
}
