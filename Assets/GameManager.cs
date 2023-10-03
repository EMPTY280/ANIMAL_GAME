using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager instance = null;

    private Fadeout fadeout;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
            instance.fadeout = GameObject.FindGameObjectWithTag("FADEOUT").GetComponent<Fadeout>();
        }
        return instance;
    }

    public void ChangeScene()
    {
        fadeout.StartFadeout(LoadScene);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Title");
        fadeout.StartFadein();
    }
}
