using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    // ���� ���� ����Ʈ
    //List<LevelSegment> segments = new List<LevelSegment>();
    List<LevelSegmentGroup> segmentGroups = new List<LevelSegmentGroup>();

    int currentMap = 0;

    // ���۽� ���ʷ� ��ġ�� ������ �ε���
    //[SerializeField] int firstSegment = 0;

    // ���� ȭ���� ���� / ���� ȭ���� ����
    LevelSegment currentSegment = null;
    LevelSegment nextSegment = null;

    // ��ũ�� �ӵ�
    [SerializeField] float scrollSpeed = 0.2f;
    // ���� ��ũ������ ������ ��ġ �� �̵� �Ӱ谪
    float offset = 0.0f;
    [SerializeField] protected float offsetMax;

    [SerializeField] protected float runDistance;

    protected void Awake()
    {
        //// ���׸�Ʈ ����Ʈ�� �ֱ�
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
        currentSegment = segmentGroups[currentMap].GetLevelSegment();
        nextSegment = segmentGroups[currentMap].GetLevelSegment();
        currentSegment.gameObject.SetActive(true);
        nextSegment.gameObject.SetActive(true);
        offsetMax = (nextSegment.GetWidth() + currentSegment.GetWidth()) / 2;
        SetSegmentPos();
    }

    // ���� ��ũ�� / ������ ȭ�� ������ �����ٸ� ���� ���� �ε�
    private void FixedUpdate()
    {
        offset += scrollSpeed * Time.fixedDeltaTime;
        runDistance += scrollSpeed * Time.fixedDeltaTime;

        if (offset >= offsetMax)
        {
            offset -= offsetMax;
            LoadNewSegment();
        }

        SetSegmentPos();
    }

    // ����Ʈ���� ������ ������ POP
    //private LevelSegment GetRandomSegment()
    //{
    //    int count = segments.Count;
    //    int randomIdx = Random.Range(0, count);

    //    LevelSegment select = segments[randomIdx];
    //    segments.RemoveAt(randomIdx);

    //    return select;
    //}

    //private LevelSegment GetNextSegment()
    //{
    //    LevelSegment select = segments[0];
    //    segments.RemoveAt(0);

    //    return select;
    //}

    // �� ������ ��ġ ������Ʈ
    private void SetSegmentPos()
    {
        currentSegment.transform.position = new Vector2(0 - offset, 0);
        nextSegment.transform.position = new Vector2(offsetMax - offset, 0);
    }

    // ���� ������� ���� ������Ʈ
    private void LoadNewSegment()
    {
        currentSegment.ReturnToOrigin();
        currentSegment.gameObject.SetActive(false);
        //segments.Add(currentSegment);
        currentSegment = nextSegment;
        //nextSegment = GetRandomSegment();
        nextSegment = segmentGroups[currentMap].GetLevelSegment();
        nextSegment.gameObject.SetActive(true);
        offsetMax = (nextSegment.GetWidth() + currentSegment.GetWidth()) / 2;
    }

    public void SetSpeed(float speed)
    {
        scrollSpeed = speed;
    }    

    public void ChangeMap(int mapNum)
    {
        currentMap = mapNum;
    }

    public float GetMapDistance()
    {
        return segmentGroups[currentMap].MapLength;
    }
}
