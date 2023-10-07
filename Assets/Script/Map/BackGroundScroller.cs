using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MapScroller
{
    SpriteRenderer mapRenderer;

    protected override void Awake()
    {
        mapRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();        
        base.Awake();
        base.offsetMax = mapRenderer.sprite.bounds.size.x * transform.localScale.x;
    }
}
