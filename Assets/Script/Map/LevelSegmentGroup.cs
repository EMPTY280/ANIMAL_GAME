using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegmentGroup : MonoBehaviour
{
    protected List<LevelSegment> segments = new List<LevelSegment>();

    private bool baseSeg = true;

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

    public virtual LevelSegment GetLevelSegment()
    {
        return GetBaseSegment();
    }

    public float GetMapSize()
    {
        SpriteRenderer mapRenderer = segments[0].GetComponent<SpriteRenderer>();
        return mapRenderer.sprite.bounds.size.x;
    }
}
