using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    // ���� ���� ����Ʈ
    List<LevelSegment> segments = new List<LevelSegment>();

    // ���۽� ���ʷ� ��ġ�� ������ �ε���
    [SerializeField] int firstSegment = 0;

    // ���� ȭ���� ���� / ���� ȭ���� ����
    LevelSegment currentSegment = null;
    LevelSegment nextSegment = null;

    // ��ũ�� �ӵ�
    [SerializeField] float scrollSpeed = 1.0f;
    // ���� ��ũ������ ������ ��ġ �� �̵� �Ӱ谪
    float offset = 0.0f;
    [SerializeField] protected float offsetMax = 22.0f;


    protected virtual void Awake()
    {
        // ���׸�Ʈ ����Ʈ�� �ֱ�
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            LevelSegment seg = transform.GetChild(i).GetComponent<LevelSegment>();
            segments.Add(seg);
        }        
    }

    private void Start()
    {
        // ���� ���� + �ι�° ���� �ε�
        currentSegment = segments[firstSegment];
        segments.RemoveAt(firstSegment);
        //nextSegment = GetRandomSegment();
        nextSegment = GetNextSegment();

        SetSegmentPos();
    }

    // ���� ��ũ�� / ������ ȭ�� ������ �����ٸ� ���� ���� �ε�
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

    // ����Ʈ���� ������ ������ POP
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
        segments.Add(currentSegment);
        currentSegment = nextSegment;
        //nextSegment = GetRandomSegment();
        nextSegment = GetNextSegment();
    }
}
