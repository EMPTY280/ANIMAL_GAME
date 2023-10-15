using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialMap : LevelSegmentGroup
{
    List<LevelSegment> reserveSegment = new List<LevelSegment>();

    [SerializeField] int delay = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    public override LevelSegment GetLevelSegment()
    {
        if(delay > 0)
        {
            delay--;
        }

        if(reserveSegment.Count > 0)
        {
            return GetReservedSegment();
        }

        return GetBaseSegment();
    }

    public void SetDelay(int _delay)
    {
        delay = _delay;
    }

    public void ReserveSegment(int segNum)
    {
        reserveSegment.Add(segments[segNum]);        
    }

    private LevelSegment GetReservedSegment()
    {
        LevelSegment temp = reserveSegment[0];
        reserveSegment.RemoveAt(0);
        return temp;
    }

    public bool InProgress()
    {
        return (delay > 0);
    }
}
