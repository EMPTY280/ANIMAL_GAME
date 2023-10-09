using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialMap : LevelSegmentGroup
{
    List<LevelSegment> reserveSegment = new List<LevelSegment>();

    int tutoSegNum = 2;
    public int reserveEnd = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    public override LevelSegment GetLevelSegment()
    {
        if(reserveSegment.Count > 0)
        {
            reserveEnd = 2;
            return GetReserveSegment();
        }
        if(reserveEnd > 0)
        {
            reserveEnd--;
        }
        return GetBaseSegment();
    }

    private LevelSegment GetReserveSegment()
    {
        LevelSegment temp = reserveSegment[0];
        reserveSegment.RemoveAt(0);
        return temp;
    }

    public void ReserveSegment()
    {
        reserveSegment.Add(segments[tutoSegNum]);        
    }

    public void QuestCheck(bool clear)
    {
        if(clear == true)
        {
            tutoSegNum++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            QuestCheck(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            QuestCheck(true);
        }
    }

    public bool GetReserveEnd()
    {
        return (reserveEnd == 0);
    }
}
