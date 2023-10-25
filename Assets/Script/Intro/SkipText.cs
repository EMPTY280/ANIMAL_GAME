using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkipText : MonoBehaviour
{
    private float delta = 0.5f * Mathf.PI;
    [SerializeField] float speed = 1.0f;
    private Text text;
    [SerializeField] float alphaMulti = 0.5f;

    private void Awake()
    {
        text = GetComponent<Text>();
        if (!text) enabled = false;
    }

    void Update()
    {
        delta += Time.deltaTime * speed;
        Color c = text.color;
        c.a = alphaMulti - Mathf.Abs(Mathf.Sin(delta + Mathf.PI)) * alphaMulti;
        text.color = c;
    }
}
