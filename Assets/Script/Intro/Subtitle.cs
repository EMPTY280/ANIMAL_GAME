using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Subtitle : MonoBehaviour
{
    private PlayableDirector pd;
    private float startDelayMax = 0.5f;
    private float startDelay = 0.0f;

    private int line = 0;
    [SerializeField] [TextArea] List<string> subs = new List<string>();
    [SerializeField] Text text = null;

    private int letterIndex = 0;
    private int letterMax = -1;

    private bool canPlay = true;
    private bool playWhenReady = false;

    private float continueDelayMax = 2.0f;
    private float continueDelay = 0.0f;

    [SerializeField] float fadeSpeed = 1.0f;
    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float fadeDelay = 1.7f;

    [SerializeField] Image skipCircle;
    [SerializeField] float skipSpeed = 1.0f;
    float skipDelay = 0.0f;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
        if (!text || !pd) enabled = false;
        text.text = " ";
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // Delay to Start Timeline
        if (startDelay < startDelayMax)
        {
            startDelay += deltaTime;
            if (startDelay >= startDelayMax)
                pd.Play();
        }

        // Skip Intro

        if (Input.anyKey)
            skipDelay += deltaTime * skipSpeed;
        else
            skipDelay = Mathf.Max(0.0f, skipDelay - deltaTime * skipSpeed);

        skipCircle.fillAmount = Mathf.Min(1.0f, skipDelay);
        Color newColor = skipCircle.color;
        newColor.a = Mathf.Min(1.0f, skipDelay) * 0.5f;
        skipCircle.color = newColor;

        if (skipDelay > 1.0f)
        {
            skipDelay = 2.0f;
            NextScene();
        }

        // Wait to Continue Subs on Typing Animation is Done.

        if (letterIndex == 0)
            continueDelay += deltaTime;

        if (continueDelay >= continueDelayMax)
        {
            if (!canPlay)
                canPlay = true;
            if (!playWhenReady)
                return;
            playWhenReady = false;
            pd.Play();
            NextLine();
        }
    }

    public void NextLine()
    {
        if (line >= subs.Count)
        {
            NextScene();
            return;
        }

        if (!canPlay)
        {
            pd.Pause();
            playWhenReady = true;
            return;
        }
        StartCoroutine(TypeAnimation());
    }

    IEnumerator TypeAnimation()
    {
        continueDelay = 0f;
        canPlay = false;

        string currentString = subs[line];
        currentString = currentString.Replace("\\n", "\n");
        letterMax = currentString.Length;

        while (letterIndex < letterMax)
        {
            letterIndex++;
            string newText = string.Copy(currentString);
            newText = newText.Insert(letterIndex, "<color=#00000000>");
            newText += "</color>";
            text.text = newText;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        letterIndex = 0;
        line++;
    }

    public void NextScene()
    {
        GameManager.Instance.ChangeScene("Title", fadeSpeed, fadeDelay, Color.white);
    }
}
