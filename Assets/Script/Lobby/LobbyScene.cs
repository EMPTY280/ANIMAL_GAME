using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Button[] subButtons;

    private bool startingGame = false;

    [SerializeField] private int depth = 0;

    [SerializeField]
    private GameObject characterSelect = null;
    [SerializeField]
    private RectTransform pointer = null;

    [SerializeField]
    private AnimationClip[] clips;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        UpdateSelectedChar();
        for (int i = 0; i < buttons.Length; i++)
        {
            Button b = buttons[i];
            string bName = string.Copy(b.name);
            b.onClick.AddListener(() => { ButtonFunc(bName); });

            ColorBlock c = b.colors;
            c.disabledColor = Color.white;
            b.colors = c;
        }

        for (int i = 0; i < subButtons.Length; i++)
        {
            Button b = subButtons[i];
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

    private void ChangeDepth(int d)
    {
        depth += d;
        foreach (Button b in buttons)
        {
            if (depth == 0)
            {
                b.interactable = true;
                b.image.raycastTarget = true;
            }
            else
            {
                b.interactable = false;
                b.image.raycastTarget = false;
            }
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
                break;
            case "ConfigMenu":
                break;
            case "Characters":
                CharacterSelect();
                break;
            case "Album":
                break;
            case "Char0":
                SetSelectedChar(Characters.RABBIT);
                break;
            case "Char1":
                SetSelectedChar(Characters.CAT);
                break;
            default:
                Quit();
                break;
        }
    }

    public void GameStart()
    {
        ChangeDepth(1);
        GameManager.Instance.ChangeScene("TutorialScene");
        startingGame = true;
    }

    private void CharacterSelect()
    {
        ChangeDepth(1);
        UpdateSelectedChar();
        characterSelect.SetActive(true);
    }

    private void UpdateSelectedChar()
    {
        if (GameManager.Instance.Character == Characters.RABBIT)
        {
            pointer.anchoredPosition = new Vector2(-200f, -75f);
            animator.SetInteger("Char", 0);
        }
        else
        {
            pointer.anchoredPosition = new Vector2(200f, -75f);
            animator.SetInteger("Char", 1);
        }
    }

    private void SetSelectedChar(Characters c)
    {
        GameManager.Instance.Character = c;
        UpdateSelectedChar();
    }

    private void Quit()
    {
        ChangeDepth(-1);
        characterSelect.SetActive(false);
    }
}
