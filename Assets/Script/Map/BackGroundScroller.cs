using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    List<LevelSegmentGroup> segmentGroups = new List<LevelSegmentGroup>();

    float[] offset = new float[3];
    float[] scrollSpeed = new float[3];

    [SerializeField] float offsetMax;

    LevelSegment[] currentSegment = new LevelSegment[3];
    LevelSegment[] nextSegment = new LevelSegment[3];

    [SerializeField] int currentMap = 0;

    private void Awake()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            LevelSegmentGroup seg = transform.GetChild(i).GetComponent<LevelSegmentGroup>();
            segmentGroups.Add(seg);
        }

        for(int i=0;i<3;i++)
        {
            offset[i] = 0;
        }     
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            currentSegment[i] = segmentGroups[i].GetLevelSegment();
            nextSegment[i] = segmentGroups[i].GetLevelSegment();
            currentSegment[i].gameObject.SetActive(true);
            nextSegment[i].gameObject.SetActive(true);
        }

        offsetMax = segmentGroups[0].GetMapSize();
    }

    private void FixedUpdate()
    {        
        for (int i = 0; i < 3; i++)
        {
            offset[i] += scrollSpeed[i] * Time.fixedDeltaTime;
            if (offset[i] >= offsetMax)
            {
                offset[i] -= offsetMax;
                LoadNewSegment(i);
            }
        }

        SetSegmentPos();
    }

    private void SetSegmentPos()
    {
        for (int i = 0; i < 3; i++)
        {
            currentSegment[i].transform.position = new Vector2(0 - offset[i], 0);
            nextSegment[i].transform.position = new Vector2(offsetMax - offset[i], 0);
        }
    }
    private void LoadNewSegment(int backGroundNum)
    {
        currentSegment[backGroundNum].ReturnToOrigin();
        currentSegment[backGroundNum].gameObject.SetActive(false);
        currentSegment[backGroundNum] = nextSegment[backGroundNum];
        nextSegment[backGroundNum] = segmentGroups[backGroundNum + (currentMap * 3)].GetLevelSegment();
        nextSegment[backGroundNum].gameObject.SetActive(true);
    }

    public void SetSpeed(float farSpeed, float middleSpeed, float closeSpeed)
    {
        scrollSpeed[0] = farSpeed;
        scrollSpeed[1] = middleSpeed;
        scrollSpeed[2] = closeSpeed;
    }

    public void MapChange(int mapNum)
    {
        currentMap = mapNum;
        for (int i = 0; i < 3; i++)
        {
            currentSegment[i].ReturnToOrigin();
            currentSegment[i].gameObject.SetActive(false);
            currentSegment[i] = segmentGroups[i + (currentMap * 3)].GetLevelSegment();
            currentSegment[i].gameObject.SetActive(true);
            nextSegment[i].ReturnToOrigin();
            nextSegment[i].gameObject.SetActive(false);
            nextSegment[i] = segmentGroups[i + (currentMap * 3)].GetLevelSegment();
            nextSegment[i].gameObject.SetActive(true);
        }
    }
}
