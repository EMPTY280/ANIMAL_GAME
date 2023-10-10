using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    // 레벨 조각 리스트
    List<LevelSegment> segments = new List<LevelSegment>();
    List<LevelSegmentGroup> segmentGroups = new List<LevelSegmentGroup>();

    int currentMap = 0;

    // 시작시 최초로 배치할 조각의 인덱스
    //[SerializeField] int firstSegment = 0;

    // 현재 화면의 조각 / 다음 화면의 조각
    LevelSegment currentSegment = null;
    LevelSegment nextSegment = null;

    // 스크롤 속도
    [SerializeField] float scrollSpeed = 0.2f;
    // 현재 스크롤중인 조각의 위치 및 이동 임계값
    float offset = 0.0f;
    [SerializeField] protected float offsetMax = 22.0f;


    protected void Awake()
    {
        //// 세그먼트 리스트에 넣기
        //int count = transform.childCount;
        //for (int i = 0; i < count; i++)
        //{
        //    LevelSegment seg = transform.GetChild(i).GetComponent<LevelSegment>();
        //    segments.Add(seg);
        //}        
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            LevelSegmentGroup seg = transform.GetChild(i).GetComponent<LevelSegmentGroup>();
            segmentGroups.Add(seg);
        }
    }

    private void Start()
    {
        //// 최초 조각 + 두번째 조각 로드
        //currentSegment = segments[firstSegment];
        //segments.RemoveAt(firstSegment);
        ////nextSegment = GetRandomSegment();
        //nextSegment = GetNextSegment();

        //SetSegmentPos();
        currentSegment = segmentGroups[currentMap].GetLevelSegment();
        nextSegment = segmentGroups[currentMap].GetLevelSegment();

        SetSegmentPos();
    }

    // 조각 스크롤 / 조각이 화면 밖으로 나갔다면 다음 조각 로드
    private void FixedUpdate()
    {
        offset += scrollSpeed;

        if (offset >= offsetMax)
        {
            offset -= offsetMax;
            LoadNewSegment();
        }

        SetSegmentPos();
    }

    // 리스트에서 무작위 조각을 POP
    private LevelSegment GetRandomSegment()
    {
        int count = segments.Count;
        int randomIdx = Random.Range(0, count);

        LevelSegment select = segments[randomIdx];
        segments.RemoveAt(randomIdx);

        return select;
    }

    private LevelSegment GetNextSegment()
    {
        LevelSegment select = segments[0];
        segments.RemoveAt(0);

        return select;
    }

    // 각 조각의 위치 업데이트
    private void SetSegmentPos()
    {
        currentSegment.transform.position = new Vector2(0 - offset, 0);
        nextSegment.transform.position = new Vector2(offsetMax - offset, 0);
    }

    // 현재 사용중인 조각 업데이트
    private void LoadNewSegment()
    {
        currentSegment.ReturnToOrigin();
        //segments.Add(currentSegment);
        currentSegment = nextSegment;
        //nextSegment = GetRandomSegment();
        nextSegment = segmentGroups[currentMap].GetLevelSegment();
    }

    public void SetSpeed(float speed)
    {
        scrollSpeed = speed;
    }
}
