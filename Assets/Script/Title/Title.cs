using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    bool isMenuActiive = false;
    [SerializeField] GameObject menu = null;
    [SerializeField] GameObject config = null;
    [SerializeField] GameObject sound = null;

    [SerializeField] Transform logo = null;
    float logoOriginYpos;
    float delta;
    [SerializeField] float logoSpeed = 1f;
    [SerializeField] float logoMoveThreshold = 10f;

    [SerializeField] GameObject pressStart = null;

    private void Awake()
    {
        menu.SetActive(false);
        config.SetActive(false);
        sound.SetActive(false);

        logoOriginYpos = logo.transform.position.y;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            ActiveTitleMenu();
        }

        delta += Time.deltaTime * logoSpeed;
        Vector2 newPos = logo.transform.position;
        newPos.y = logoOriginYpos + Mathf.Cos(delta) * logoMoveThreshold;
        logo.position = newPos;
    }

    private void ActiveTitleMenu()
    {
        if (!isMenuActiive)
        {
            isMenuActiive = true;
            GameManager.Instance.ForceFadeIn();
            GameManager.Instance.SetBlackout(true);
            pressStart.SetActive(false);
            logo.gameObject.SetActive(false);
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
        GameManager.Instance.ChangeScene("Lobby");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartTutorial()
    {
        GameManager.Instance.ChangeScene("TutorialScene");
    }
}
