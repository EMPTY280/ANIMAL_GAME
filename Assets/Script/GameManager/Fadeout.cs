using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public delegate void Callback();

    private Image fadeout;
    private float fadeSpeed = 1.0f;
    private float transitionDelay = 0.5f;

    private Coroutine task = null;

    public float FadeSpeed
    {
        get { return fadeSpeed; }
        set { fadeSpeed = value; }
    }

    public Color FadeColor
    {
        set
        {
            value.a = fadeout.color.a;
            fadeout.color = value;
        }
    }

    public float TransitionDelay
    {
        set { transitionDelay = value; }
    }

    private void Awake()
    {
        fadeout = GetComponent<Image>();
    }

    public void StartFadeout(Callback c)
    {
        task = StartCoroutine(FadeoutCoroutine(c));
    }

    public void StartFadein()
    {
        task = StartCoroutine(FadeinCoroutine());
    }

    public void ForceFadeIn()
    {
        if (task == null) return;

        StopCoroutine(task);
        fadeout.color = new Color(0f, 0f, 0f, 0f);
    }

    IEnumerator FadeoutCoroutine(Callback c)
    {
        while (fadeout.color.a < 1)
        {
            Color newColor = fadeout.color;
            newColor.a += Time.deltaTime * fadeSpeed;
            fadeout.color = newColor;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(transitionDelay);
        task = null;
        c();
    }

    IEnumerator FadeinCoroutine()
    {
        while (fadeout.color.a > 0)
        {
            Color newColor = fadeout.color;
            newColor.a -= Time.deltaTime * fadeSpeed;
            fadeout.color = newColor;
            yield return null;
        }
        task = null;
    }
}
