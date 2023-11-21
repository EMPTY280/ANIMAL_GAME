using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Re_PlayUI : MonoBehaviour
{
    [SerializeField] protected Button[] buttons;
    [SerializeField] RectTransform PausePopUp;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI clearItem;
    [SerializeField] Image[] hpUI = new Image[3];
    [SerializeField] Slider processBar;

    protected virtual void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            string buttonName = buttons[i].name;
            buttons[i].onClick.AddListener(() => { OnClickButton(buttonName); });
        }        
    }

    protected virtual void Start()
    {
        Re_PlaySceneManager.Instance.AddListener(EVENT_TYPE.HP_CHANGED, OnHpChanged);
    }

    void OnClickButton(string buttonName)
    {
        ButtonFunction(buttonName);
    }

    protected virtual void ButtonFunction(string buttonName)
    {
        switch (buttonName)
        {
            case "PauseButton":
                if (GameManager.Instance.IsChangingScene)
                    break;
                PausePopUp.gameObject.SetActive(true);
                GameManager.Instance.SetBlackout(true);
                GameManager.Instance.SetPause(true);
                break;

            case "Continue":
                PausePopUp.gameObject.SetActive(false);
                StartCoroutine(ContinueTimer(3));
                break;

            case "Exit":
                GameManager.Instance.SetBlackout(false);
                PausePopUp.gameObject.SetActive(false);
                GameManager.Instance.SetPause(false);
                GameManager.Instance.ChangeScene("Title");
                break;
        }
    }

    void OnHpChanged(EVENT_TYPE eventType, Component sender, object param = null)
    {
        Re_Player player = sender.gameObject.GetComponent<Re_Player>();
        int hp = player.CurrentHp - 1;

        for (int i = 0; i < 3; i++)
        {
            if (i <= hp)
            {
                if (hpUI[i].gameObject.activeSelf == false)
                    hpUI[i].gameObject.SetActive(true);
            }
            else
            {
                if (hpUI[i].gameObject.activeSelf == true)
                    hpUI[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetClearItem(int _clearItem)
    {
        clearItem.text = _clearItem.ToString();
    }

    public void SetProcess(float process)
    {
        processBar.value = process;
    }

    IEnumerator ContinueTimer(int sec)
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        timer.gameObject.SetActive(true);

        for (int i = sec; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return delay;
        }

        timer.gameObject.SetActive(false);
        GameManager.Instance.SetBlackout(false);
        GameManager.Instance.SetPause(false);
    }
}
