using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipText : MonoBehaviour
{
    private float delta = 0.0f;
    [SerializeField] float speed = 1.0f;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        if (!text) enabled = false;
    }

    void Update()
    {
        delta += Time.deltaTime * speed;
        Color c = text.color;
        c.a =  Mathf.Max(Mathf.Sin(delta + Mathf.PI), 0.0f);
        text.color = c;
    }
}