using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Re_MapManager : MonoBehaviour
{
    [SerializeField] Re_MapScroller mapScroller;
    [SerializeField] Re_BackGroundScroller[] bgScrollers = new Re_BackGroundScroller[3];
    [SerializeField] List<Re_SegmentGroup> maps = new List<Re_SegmentGroup>();
    [SerializeField] SpriteRenderer fadePanel;

    [SerializeField] float chapterDistance = 600f;
    [SerializeField] float distance = 0f;

    [Header("Speed")]
    [SerializeField] private float mapSegment = 1f;
    [SerializeField] private float farBackground = 1f;
    [SerializeField] private float middleBackground = 1f;
    [SerializeField] private float closeBackground = 1f;

    float totalDistance = 0;
    float chapterMeasure = 0f;
    float originMapSpeed = 10f;
    float times = 1f;

    bool isMeasuringDistance = true;
    int currentMap = 0;
    public int CurrentMap => currentMap;
    public float Distance { get { return distance; } set { distance = value; Re_PlaySceneManager.Instance.SetProcess(distance / totalDistance); } }
    public float Times { set {  times = value; UpdateSpeed(); } }

    private void Start()
    {
        Re_PlaySceneManager.Instance.AddListener(EVENT_TYPE.RUN_START, OnStartRun);
        Re_PlaySceneManager.Instance.AddListener(EVENT_TYPE.RUN_END, OnEndRun);
        Re_PlaySceneManager.Instance.AddListener(EVENT_TYPE.PLAYER_DEAD, OnPlayerDead);
        int count = maps.Count;
        for (int i = 0; i < count; i++)
        {
            totalDistance += maps[i].ChapterLength;
        }
        chapterDistance = maps[0].ChapterLength;
        UpdateSpeed();
        MapReserve();
    }

    private void Update()
    {
        if (isMeasuringDistance == true)
        {
            Distance += originMapSpeed * times * Time.deltaTime;// 거리 측정
            chapterMeasure += originMapSpeed * times * Time.deltaTime;
            if (chapterMeasure >= chapterDistance)
            {
                chapterMeasure = 0f;
                isMeasuringDistance = false;
            }
        }
        else if (isMeasuringDistance == false && Distance < (currentMap + 1) * chapterDistance && Distance > 0)
        {
            Distance += originMapSpeed * times * Time.deltaTime;// 거리 측정
        }
    }

    void UpdateSpeed()
    {
        mapScroller.ScrollSpeed = originMapSpeed * times * mapSegment;
        bgScrollers[0].ScrollSpeed = originMapSpeed * times * farBackground;
        bgScrollers[1].ScrollSpeed = originMapSpeed * times * middleBackground;
        bgScrollers[2].ScrollSpeed = originMapSpeed * times * closeBackground;
    }

    IEnumerator ChangeMap()
    {        
        Color color = fadePanel.color;
        WaitForSeconds delay = new WaitForSeconds(0.01f);
        while (color.a < 1.0f)
        {
            color.a += 0.01f;
            yield return delay;
            fadePanel.color = color;
        }

        yield return new WaitForSeconds(1.5f);

        LoadNextMap();        

        while (color.a > 0f)
        {
            color.a -= 0.01f;
            yield return delay;
            fadePanel.color = color;
        }
    }

    void LoadNextMap()
    {
        currentMap++; // change to next segmentGroup and BackGround with FadeOut
        chapterDistance = maps[currentMap].ChapterLength;
        mapScroller.BaseGround = maps[currentMap].GroundSprite;
        bgScrollers[0].ChangeMap(currentMap);
        bgScrollers[1].ChangeMap(currentMap);
        bgScrollers[2].ChangeMap(currentMap);
        MapReserve();
    }

    void MapReserve()
    {
        float disSum = 0f;

        if (maps[currentMap].MapLength < chapterDistance) return;

        for(int i = 0; disSum < chapterDistance ; i++)
        {
            if (i > 100) break;
            Re_MapSegment seg = maps[currentMap].GetRandomSegment(0);
            disSum += seg.SegmentLength;
            mapScroller.ReserveSegment(seg);
        }
    }

    void OnStartRun(EVENT_TYPE eventType, Component sender, object param = null)
    {
        isMeasuringDistance = true;
    }

    void OnEndRun(EVENT_TYPE eventType, Component sender, object param = null)
    {
        isMeasuringDistance = false;
        if (currentMap == maps.Count - 1)
        {
            Re_PlaySceneManager.Instance.GameOver();
        }
        else
        {
            StartCoroutine(ChangeMap());
        }
    }

    void OnPlayerDead(EVENT_TYPE eventType, Component sender, object param = null)
    {
        Times = 0f;
        isMeasuringDistance = false;
    }
}
