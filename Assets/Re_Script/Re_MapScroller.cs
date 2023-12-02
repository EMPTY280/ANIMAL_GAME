using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_MapScroller : MonoBehaviour
{
    [SerializeField] protected Re_MapManager mapManager;
    [SerializeField] BaseSegment[] baseSegment = new BaseSegment[2];
    [SerializeField] List<Re_MapSegment> reservedSegment = new List<Re_MapSegment>();

    Re_MapSegment firstSegment = null;
    Re_MapSegment secondSegment = null;

    Re_MapSegment firstReservedSegment = null;
    Re_MapSegment lastReservedSegment = null;

    Sprite baseGround = null;
    public Sprite BaseGround { set { baseGround = value; changeGroundCount = 2; } }
        
    protected int currentBase = 0;
    public int changeGroundCount = 0;

    protected float scrollSpeed = 10f;
    protected float offset = 0f;
    protected float interval;

    public float ScrollSpeed { set { scrollSpeed = value; } }

    protected virtual void Start()
    {
        firstSegment = LoadBaseSegment();
        secondSegment = LoadBaseSegment();

        firstSegment.gameObject.SetActive(true);
        secondSegment.gameObject.SetActive(true);

        interval = (firstSegment.SegmentLength + secondSegment.SegmentLength) / 2;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        offset += scrollSpeed * Time.deltaTime;

        if (offset >= interval)
        {
            offset -= interval;
            SegmentUpdate();
        }

        firstSegment.transform.position = new Vector2(0 - offset, 0);
        secondSegment.transform.position = new Vector2(interval - offset, 0);
    }

    protected virtual void SegmentUpdate()
    {
        if (lastReservedSegment != null && firstSegment == lastReservedSegment)
        {            
            lastReservedSegment = null;
            EndRun();
        }

        if (firstReservedSegment != null && secondSegment == firstReservedSegment)
        {
            firstReservedSegment = null;
            StartRun();
        }

        firstSegment.ResetSegment();
        firstSegment = secondSegment;
        
        if (reservedSegment.Count > 0)
        {
            secondSegment = reservedSegment[0];
            if (reservedSegment.Count == 1)
            {                
                lastReservedSegment = secondSegment;
            }
            reservedSegment.RemoveAt(0);
        }
        else
        {
            secondSegment = LoadBaseSegment();            
        }

        secondSegment.gameObject.SetActive(true);
        interval = (firstSegment.SegmentLength + secondSegment.SegmentLength) / 2;
    }

    void StartRun() // when isRunning == true reservedSegment Update
    {
        Re_PlaySceneManager.Instance.NoticePost(EVENT_TYPE.RUN_START, this);
    }

    void EndRun() // when isRunning == false reservedSegment Return
    {
        Re_PlaySceneManager.Instance.NoticePost(EVENT_TYPE.RUN_END, this);
    }

    public void ReserveSegment(Re_MapSegment segment)
    {
        if (firstReservedSegment == null) firstReservedSegment = segment;        
        reservedSegment.Add(segment);
    }

    public void InsertSegment(Re_MapSegment segment)
    {
        reservedSegment.Insert(0, segment);
    }

    Re_MapSegment LoadBaseSegment()
    {
        if (currentBase > 1)
            currentBase = 0;
        if (changeGroundCount-- > 0)
            ChangeBaseSegment(baseSegment[currentBase]);
        return baseSegment[currentBase++];
    }

    private void ChangeBaseSegment(BaseSegment baseSeg)
    {
        baseSeg.ChangeGround(baseGround);
    }

    public bool ReservedCheck(Re_MapSegment segment)
    {
        int count = reservedSegment.Count;

        for(int i = 0; i < count; i++)
        {
            if (reservedSegment[i] == segment) return false;
        }

        return true;
    }

    //public void GetSpecificSegment(int level, int segmentNumber)
    //{
    //    Re_MapSegment mapSegment = maps[currentMap].GetSpecificSegment(level, segmentNumber);
    //    mapSegment.gameObject.SetActive(true);
    //    reservedSegment.Add(mapSegment);
    //}

    //public void GetRandomSegment()
    //{
    //    Re_MapSegment mapSegment = maps[currentMap].GetRandomSegment();
    //    mapSegment.gameObject.SetActive(true);
    //    reservedSegment.Add(mapSegment);
    //}

    //public void GetRandomSegment(int level)
    //{
    //    Re_MapSegment mapSegment = maps[currentMap].GetRandomSegment(level);
    //    mapSegment.gameObject.SetActive(true);
    //    reservedSegment.Add(mapSegment);
    //}

    //public void GetCurrentSegment()
    //{
    //    Re_MapSegment mapSegment = maps[currentMap].GetCurrentSegment();
    //    mapSegment.gameObject.SetActive(true);
    //    reservedSegment.Add(mapSegment);
    //}

    //public void GetSegmentSequentially()
    //{
    //    Re_MapSegment mapSegment = maps[currentMap].GetSegmentSequentially();
    //    mapSegment.gameObject.SetActive(true);
    //    reservedSegment.Add(mapSegment);
    //}
}
