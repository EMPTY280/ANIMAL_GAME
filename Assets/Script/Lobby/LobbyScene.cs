using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    private bool startingGame = false;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button b = buttons[i];
            string bName = string.Copy(b.name);
            b.onClick.AddListener(() => { ButtonFunc(bName); });
        }
    }

    private void Update()
    {
        if (Input.anyKey && !startingGame)
        {
            GameManager.Instance.ForceFadeIn();
        }
    }

    public void ButtonFunc(string name)
    {
        switch(name)
        {
            case "StartGame":
                GameStart();
                break;
            case "PostBox":
            case "ConfigMenu":
            case "Characters":
            case "Album":
            default:
                break;
        }
    }

    public void GameStart()
    {
        GameManager.Instance.ChangeScene("TutorialScene");
        startingGame = true;
    }
}
