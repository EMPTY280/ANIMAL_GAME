using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct GameResult
{
    public int collectedItems;
    public int collectGoal;
    public string stageName;
    public bool isClear;
}

public class GameManager : ScriptableObject
{
    private static GameManager instance = null;

    private Fadeout fadeout;
    private string sceneTransitionTarget = null;
    private bool isChangingScene = false;
    private bool isFadinig = false;

    public bool IsChangingScene
    {
        get { return isChangingScene; }
    }

    public bool IsFading
    {
        get { return isFadinig; }
    }

    private Blackout blackout;
    private Spotlight spotlight = null;

    private GameResult lastGame;

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

                instance.lastGame.stageName = "STAGE_NAME";
                instance.lastGame.collectedItems = 99;
                instance.lastGame.collectGoal = 99;
                instance.lastGame.isClear = true;
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
        if (isChangingScene || isFadinig) return;

        fadeout.FadeSpeed = fadeSpeed;
        fadeout.FadeColor = color ?? Color.black;
        fadeout.TransitionDelay = delay;

        sceneTransitionTarget = sceneName;
        isChangingScene = true;
        isFadinig = true;

        fadeout.StartFadeout(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneTransitionTarget);
        blackout.SetBlackout(false);
        fadeout.StartFadein(() => { isFadinig = false; });
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
        if (!isFadinig) return;

        fadeout.ForceFadeIn();
        isFadinig = false;
        isChangingScene = false;
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

    public void SetBlackout(bool active, Vector3 position, Vector2 size)
    {
        spotlight.SetRect(position, size);
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

    /// <summary>
    /// Save Game Result
    /// </summary>
    /// <param name="game">structure that contains collectedItems, stageName, and isClear</param>
    public void SaveGameResult(GameResult game)
    {
        lastGame.collectedItems = game.collectedItems;
        lastGame.collectGoal = game.collectGoal;
        lastGame.stageName = game.stageName;
        lastGame.isClear = game.isClear;
    }

    /// <summary>
    /// Get Result of Latest Game
    /// </summary>
    public GameResult GetLastGameResult()
    {
        return lastGame;
    }
}
