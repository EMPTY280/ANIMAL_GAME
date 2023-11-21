using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Ground : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    public SpriteRenderer GetSprite => spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.size = new Vector2(spriteRenderer.size.x, boxCollider.size.y);
        boxCollider.offset = new Vector2(0, boxCollider.size.y * (-0.5f));
    }
}
