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
    public bool IsChangingScene
    {
        get { return isChangingScene; }
    }

    private Blackout blackout;
    private Spotlight spotlight = null;

    /// <summary>
    /// Returns GameManager singleton instance.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateInstance<GameManager>();

                GameObject prefab = Resources.Load<GameObject>("Prefabs/ScreenEffect");
                GameObject newInst = Instantiate(prefab);

                instance.fadeout = newInst.GetComponentInChildren<Fadeout>();
                instance.blackout = newInst.GetComponentInChildren<Blackout>();
                instance.spotlight = newInst.GetComponentInChildren<Spotlight>();
            }
            return instance;
        }
    }

    /// <summary>
    /// Change scene with fade-out.
    /// </summary>
    /// <param name="sceneName">Name of scene to transit</param>
    /// <param name="fadeSpeed">Speed of fade. (1 sec * speed)</param>
    /// <param name="delay">Wating time between fade-in and fade-out</param>
    /// <param name="color">Fading color</param>
    public void ChangeScene(string sceneName, float fadeSpeed = 1.0f, float delay = 0.5f, Color? color = null)
    {
        if (isChangingScene) return;

        fadeout.FadeSpeed = fadeSpeed;
        fadeout.FadeColor = color ?? Color.black;
        fadeout.TransitionDelay = delay;

        sceneTransitionTarget = sceneName;
        isChangingScene = true;

        fadeout.StartFadeout(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneTransitionTarget);
        blackout.SetBlackout(false);
        fadeout.StartFadein();
        isChangingScene = false;
    }

    /// <summary>
    /// Change scene immediately.
    /// </summary>
    /// <param name="sceneName">Name of scene to transit</param>
    public void ChangeSceneWithoutFade(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Force fadein effect to end.
    /// </summary>
    public void ForceFadeIn()
    {
        fadeout.ForceFadeIn();
    }

    /// <summary>
    /// Slightly Darkens the Screen.
    /// </summary>
    /// <param name="active">Activates if it's true</param>
    /// <param name="rect">Activates Spotlight effect.</param>
    public void SetBlackout(bool active, Rect? rect = null)
    {
        spotlight.SetRect(rect ?? Rect.zero);
        blackout.SetBlackout(active);
    }

    /// <summary>
    /// Pauses Game
    /// </summary>
    /// <param name="active">Activates if it's true</param>
    public void SetPause(bool active)
    {
        Time.timeScale = active ? 0.0f : 1.0f;
    }
}
