using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    bool isMenuActiive = false;
    [SerializeField] GameObject menu = null;
    [SerializeField] GameObject config = null;
    [SerializeField] GameObject sound = null;

    private void Awake()
    {
        menu.SetActive(false);
        config.SetActive(false);
        sound.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            ActiveTitleMenu();
        }
    }

    private void ActiveTitleMenu()
    {
        if (!isMenuActiive)
        {
            isMenuActiive = true;
            GameManager.GetInstance().SetBlackout(true);
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        config.SetActive(false);
        sound.SetActive(false);
    }

    public void OpenConfig()
    {
        menu.SetActive(false);
        config.SetActive(true);
        sound.SetActive(false);
    }

    public void OpenSoundMenu()
    {
        menu.SetActive(false);
        config.SetActive(false);
        sound.SetActive(true);
    }

    public void StartGame()
    {
        GameManager.GetInstance().ChangeScene("TutorialScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
