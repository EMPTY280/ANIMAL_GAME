using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTiling : MonoBehaviour
{
    private void Awake()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(transform.GetComponent<SpriteRenderer>().size.x, col.size.y);
    }
}