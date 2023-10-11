using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ScriptableObject
{
    private static GameManager instance = null;

    private Fadeout fadeout;
    private string sceneTransitionTarget = null;
    private bool isChangingScene = false;

    private Blackout blackout;

    // Get Singleton Instance
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = CreateInstance<GameManager>();

            GameObject prefab = Resources.Load<GameObject>("Prefabs/ScreenEffect");
            GameObject newInst = Instantiate(prefab);

            instance.fadeout = newInst.GetComponentInChildren<Fadeout>();
            instance.blackout = newInst.GetComponentInChildren<Blackout>();

            if (!instance.fadeout) Debug.Log("asd");
            if (!instance.blackout) Debug.Log("ddd");
        }
        return instance;
    }

    // Change Scene with Fade-Out
    // (Fade-Out Part)
    // [ Name of Scene to Change ], [Fade-In/Out Speed, (1 sec * speed)], [Delay Between Fade-In and Out] [Fade Color]
    public void ChangeScene(string sceneName, float fadeSpeed = 1.0f, float delay = 0.5f, float r = 0.0f, float g = 0.0f, float b = 0.0f)
    {
        if (isChangingScene) return;

        fadeout.FadeSpeed = fadeSpeed;
        fadeout.FadeColor = new Color(r, g, b);
        fadeout.TransitionDelay = delay;

        sceneTransitionTarget = sceneName;
        isChangingScene = true;

        fadeout.StartFadeout(LoadScene);
    }

    // Change Scene with Fade-Out
    // (Fade-Out + Scene Transition Part)
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneTransitionTarget);
        blackout.SetBlackout(false);
        fadeout.StartFadein();
        isChangingScene = false;
    }

    public void SetBlackout(bool active)
    {
        blackout.SetBlackout(active);
    }
}
