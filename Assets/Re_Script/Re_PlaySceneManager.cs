using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public enum EVENT_TYPE // 이벤트 종류
{
    RUN_START,
    RUN_END,
    PLAYER_DEAD,
    HP_CHANGED
};

public class Re_PlaySceneManager : MonoBehaviour
{
    // Manager Scripts
    [SerializeField] Re_MapManager mapManager;
    [SerializeField] Re_Player player;
    [SerializeField] Re_PlayUI ui;

    private int clearItem = 0;
    public int ClearItem { get { return clearItem; } set { clearItem = value; ui.SetClearItem(clearItem); } }

    public static Re_PlaySceneManager Instance => instance;
    private static Re_PlaySceneManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void GameOver()
    {
        GameResult gameResult = new GameResult();
        gameResult.collectedItems = ClearItem;
        gameResult.collectGoal = 10;
        gameResult.isClear = (ClearItem >= gameResult.collectGoal); // && run Complete
        gameResult.stageName = "1111";
        GameManager.Instance.SaveGameResult(gameResult);
        GameManager.Instance.ChangeScene("GameResult");
    }

    public void SetSpeed(float times)
    {
        mapManager.Times = times;
    }

    public void SetProcess(float process)
    {
        ui.SetProcess(process);
    }

    #region EventProcess

    public delegate void OnEvent(EVENT_TYPE eventType, Component sender, object param = null);

    private Dictionary<EVENT_TYPE, List<OnEvent>> listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>(); 

    public void AddListener(EVENT_TYPE eventType, OnEvent listener)
    {
        List<OnEvent> listenList = null;

        if (listeners.TryGetValue(eventType, out listenList))
        {
            listenList.Add(listener);
            return;
        }

        listenList = new List<OnEvent>();
        listenList.Add(listener);
        listeners.Add(eventType, listenList);        
    }

    public void NoticePost(EVENT_TYPE eventType, Component sender, object param = null)
    {
        List<OnEvent> listenList = null;

        if (!listeners.TryGetValue(eventType, out listenList))
            return;

        for (int i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
            {
                listenList[i](eventType, sender, param);
            }
        }
    }

    public void RemoveEvent(EVENT_TYPE eventType)
    {
        listeners.Remove(eventType);
    }

    #endregion
}
