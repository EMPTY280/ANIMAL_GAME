using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSegment : Re_MapSegment
{
    Re_Ground _ground;

    protected override void Awake()
    {
        base.Awake();
        _ground = transform.GetChild(0).GetComponent<Re_Ground>();
    }

    public void ChangeGround(Sprite ground)
    {
        _ground.GetSprite.sprite = ground;
    }
}
