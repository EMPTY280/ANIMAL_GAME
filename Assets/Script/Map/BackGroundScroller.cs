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

    //LevelSegment far_1 = null;
    //LevelSegment far_2 = null;

    //LevelSegment middle_1 = null;
    //LevelSegment middle_2 = null;

    //LevelSegment close_1 = null;
    //LevelSegment close_2 = null;

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
        }

        offsetMax = segmentGroups[0].GetMapSize();

        //far_1 = segmentGroups[0].GetLevelSegment();
        //far_2 = segmentGroups[0].GetLevelSegment();

        //middle_1 = segmentGroups[1].GetLevelSegment();
        //middle_2 = segmentGroups[1].GetLevelSegment();

        //close_1 = segmentGroups[2].GetLevelSegment();
        //close_2 = segmentGroups[2].GetLevelSegment();
    }

    private void FixedUpdate()
    {        
        for (int i = 0; i < 3; i++)
        {
            offset[i] += scrollSpeed[i];
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

        //far_1.transform.position = new Vector2(0 - offset[0], 0);
        //far_2.transform.position = new Vector2(offsetMax - offset[0], 0);

        //middle_1.transform.position = new Vector2(0 - offset[1], 0);
        //middle_2.transform.position = new Vector2(offsetMax - offset[1], 0);

        //close_1.transform.position = new Vector2(0 - offset[2], 0);
        //close_2.transform.position = new Vector2(offsetMax - offset[2], 0);
    }
    private void LoadNewSegment(int backGroundNum)
    {
        currentSegment[backGroundNum].ReturnToOrigin();
        currentSegment[backGroundNum] = nextSegment[backGroundNum];
        nextSegment[backGroundNum] = segmentGroups[backGroundNum].GetLevelSegment();
    }

    public void SetSpeed(float speed)
    {
        scrollSpeed[0] = speed / 4;
        scrollSpeed[1] = speed / 3;
        scrollSpeed[2] = speed / 2;
    }
}
