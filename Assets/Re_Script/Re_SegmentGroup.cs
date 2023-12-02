using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_SegmentGroup : MonoBehaviour
{
    Sprite groundSprite; // 맵 스크롤러에서 받아올 타일 정보
    public Sprite GroundSprite => groundSprite;

    Dictionary<int, List<Re_MapSegment>> segments = new Dictionary<int, List<Re_MapSegment>>(); // 레벨 별 세그먼트 리스트 0, 1, 2

    private float mapLength = 0f;
    [SerializeField] private float chapterLength = 600f;
    public float MapLength => mapLength;
    public float ChapterLength => chapterLength;

    int levelMax = 3;
    int currentLevel = 0;
    int currentSegmentNumber = 0;

    Re_MapSegment lastReturnedSegment = null;
    Re_MapSegment beforeLastSegment = null;

    private void Awake()
    {
        for (int i = 0; i < levelMax; i++)
        {
            segments.Add(i, new List<Re_MapSegment>());
        }

        int count = transform.childCount;

        SpriteRenderer baseGround = transform.GetChild(0).GetComponent<SpriteRenderer>();
        groundSprite = baseGround.sprite;

        for (int i = 1; i < count; i++)
        {
            Re_MapSegment childSeg = transform.GetChild(i).GetComponent<Re_MapSegment>();
            segments[childSeg.SegmentLevel].Add(childSeg);
            mapLength += childSeg.SegmentLength;
            childSeg.gameObject.SetActive(false);
        }
    }

    #region GetSegment

    public Re_MapSegment GetSpecificSegment(int level, int segmentNumber)
    {
        if (segments[level][segmentNumber] == lastReturnedSegment)
        {
            return null;
        }
        lastReturnedSegment = segments[level][segmentNumber];
        return segments[level][segmentNumber];
    }

    public Re_MapSegment GetRandomSegment() // 모든 세그먼트 중 랜덤으로 하나를 반환
    {
        int level = Random.Range(0, levelMax);
        while (segments[level].Count == 0)
        {
            level = Random.Range(0, levelMax);
        }
        int segmentNumber = Random.Range(0, segments[level].Count);
        if (segments[level][segmentNumber] == lastReturnedSegment)
        {
            return GetRandomSegment();
        }
        lastReturnedSegment = segments[level][segmentNumber];
        return segments[level][segmentNumber];
    }

    public Re_MapSegment GetRandomSegment(int level) // 특정 레벨 세그먼트 중 랜덤으로 하나를 반환
    {
        int segmentNumber = Random.Range(0, segments[level].Count);
        if (segments[level][segmentNumber] == lastReturnedSegment || segments[level][segmentNumber] == beforeLastSegment)
        {
            if (segments[level].Count < 3)
                return null;
            else
                return GetRandomSegment(level);
        }
        beforeLastSegment = lastReturnedSegment;
        lastReturnedSegment = segments[level][segmentNumber];
        return segments[level][segmentNumber];
    }

    public Re_MapSegment GetCurrentSegment() // 순차적으로 반환 되었던 마지막 세그먼트를 다시 반환
    {
        if (segments[currentLevel][currentSegmentNumber].gameObject.activeSelf == true)
        {
            return null;
        }
        lastReturnedSegment = segments[currentLevel][currentSegmentNumber];
        return segments[currentLevel][currentSegmentNumber];
    }

    //public Re_MapSegment GetSegmentSequentially() // 레벨 0 첫 번째 세그먼트부터 순차적으로 반환
    //{
    //    if (currentSegmentNumber >= segments[currentLevel].Count)
    //    {
    //        currentSegmentNumber = 0;
    //        currentLevel++;

    //        if (currentLevel >= levelMax)
    //        {
    //            return null;
    //        }
    //    }
    //    if (segments[currentLevel][currentSegmentNumber].gameObject.activeSelf == true)
    //    {
    //        return null;
    //    }
    //    return segments[currentLevel][currentSegmentNumber++];
    //}

    #endregion
}
