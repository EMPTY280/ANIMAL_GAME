using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySceneUI : UIBase
{
    [SerializeField] RectTransform PausePopUp;
    [SerializeField] TextMeshProUGUI timer;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonFunction(string buttonName)
    {
        base.ButtonFunction(buttonName);
        switch (buttonName)
        {
            case "PauseButton":
                if (GameManager.Instance.IsChangingScene)
                    break;
                PausePopUp.gameObject.SetActive(true);
                //Time.timeScale = 0f;
                GameManager.Instance.SetBlackout(true);
                GameManager.Instance.SetPause(true);
                break;

            case "Continue":
                PausePopUp.gameObject.SetActive(false);
                StartCoroutine(ContinueTimer(3));
                //Time.timeScale = 1f;
                //GameManager.Instance.SetBlackout(false);
                //GameManager.Instance.SetPause(false);
                break;

            case "Exit":
                // 로비 화면으로 씬 전환
                GameManager.Instance.SetBlackout(false);
                PausePopUp.gameObject.SetActive(false);
                GameManager.Instance.SetPause(false);
                GameManager.Instance.ChangeScene("Lobby");
                break;
        }
    }

    IEnumerator ContinueTimer(int sec)
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        timer.gameObject.SetActive(true);

        for(int i = sec; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return delay;
        }

        timer.gameObject.SetActive(false);
        GameManager.Instance.SetBlackout(false);
        GameManager.Instance.SetPause(false);
    }
}
