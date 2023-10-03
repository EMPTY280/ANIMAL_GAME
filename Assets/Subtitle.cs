using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Subtitle : MonoBehaviour
{
    private PlayableDirector pd;

    private int line = 0;
    [SerializeField] [TextArea] List<string> subs = new List<string>();
    [SerializeField] Text text = null;


    private int letterIndex = 0;
    private int letterMax = -1;

    bool canPlay = true;
    bool playWhenReady = false;

    float delayMax = 2.0f;
    float delay = 0f;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
        if (!text || !pd) enabled = false;
        text.text = " ";
    }

    private void Update()
    {
        if (letterIndex == 0)
            delay += Time.deltaTime;

        if (delay >= delayMax)
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
        delay = 0f;
        canPlay = false;

        string currentString = subs[line];
        currentString = currentString.Replace("\\n", "\n");
        letterMax = currentString.Length;

        while (letterIndex < letterMax)
        {
            letterIndex++;
            string newText = string.Copy(currentString);
            newText = newText.Insert(letterIndex, "<color=black>");
            newText += "</color>";
            text.text = newText;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        letterIndex = 0;
        line++;
    }

    public void NextScene()
    {
        text.text = " ";
        GameManager.GetInstance().ChangeScene();
    }
}
