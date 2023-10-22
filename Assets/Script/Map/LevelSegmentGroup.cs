using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegmentGroup : MonoBehaviour
{
    protected List<LevelSegment> segments = new List<LevelSegment>();

    private bool baseSeg = true;
    [SerializeField] int mapLength = 0;
    [SerializeField] float process = 0;
    //int currentMapNum = -1;
    int currentMapNum = 5;

    protected virtual void Awake()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            LevelSegment seg = transform.GetChild(i).GetComponent<LevelSegment>();
            segments.Add(seg);
        }
    }

    protected void Start()
    {
        int count = segments.Count;
        for (int i = 0; i < count; i++)
        {
            segments[i].gameObject.SetActive(false);
        }
    }

    protected LevelSegment GetBaseSegment()
    {
        if(baseSeg == true)
        {
            baseSeg = false;
            return segments[0];
        }
        else
        {
            baseSeg = true;
            return segments[1];
        }        
    }

    protected LevelSegment GetNextSegment()
    {
        return segments[currentMapNum];
    }

    public virtual LevelSegment GetLevelSegment()
    {
        currentMapNum++;
        if (currentMapNum < mapLength)
        {
            return GetNextSegment();
        }
        else
        {
            return GetBaseSegment();
        }
    }

    public float GetMapSize()
    {
        SpriteRenderer mapRenderer = segments[0].GetComponent<SpriteRenderer>();
        return mapRenderer.sprite.bounds.size.x;
    }

    public float MapProcess()
    {
        process = ((float)currentMapNum / (float)mapLength);
        return ((float)currentMapNum / (float)mapLength);
    }
}
