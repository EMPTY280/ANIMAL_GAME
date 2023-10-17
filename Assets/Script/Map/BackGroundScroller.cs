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
        currentSegment[backGroundNum] = nextSegment[backGroundNum];
        nextSegment[backGroundNum] = segmentGroups[backGroundNum].GetLevelSegment();
    }

    public void SetSpeed(float speed)
    {
        scrollSpeed[0] = speed / 8;
        scrollSpeed[1] = speed / 4;
        scrollSpeed[2] = speed / 2;
    }
}
