using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public delegate void Callback();

    private Image fadeout;
    private float fadeSpeed = 1.0f; 

    private void Awake()
    {
        fadeout = GetComponent<Image>();
        DontDestroyOnLoad(transform.parent);
    }

    public void StartFadeout(Callback c)
    {
        StartCoroutine(FadeoutCoroutine(c));
    }

    public void StartFadein()
    {
        StartCoroutine(FadeinCoroutine());
    }

    IEnumerator FadeoutCoroutine(Callback c)
    {
        while (fadeout.color.a < 1)
        {
            fadeout.color = new Color(0, 0, 0, fadeout.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }
        c();
    }

    IEnumerator FadeinCoroutine()
    {
        while (fadeout.color.a > 0)
        {
            fadeout.color = new Color(0, 0, 0, fadeout.color.a - Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }
}
