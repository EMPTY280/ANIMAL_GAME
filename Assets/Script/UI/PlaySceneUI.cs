using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySceneUI : UIBase
{
    [SerializeField] RectTransform PausePopUp;

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
                PausePopUp.gameObject.SetActive(true);
                //Time.timeScale = 0f;
                GameManager.Instance.SetPause(true);
                break;

            case "Continue":
                PausePopUp.gameObject.SetActive(false);
                //Time.timeScale = 1f;
                GameManager.Instance.SetPause(false);
                break;

            case "Exit":
                // 로비 화면으로 씬 전환
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ButtonFunction("PauseButton");
        }
    }
}
