using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // �����Ϳ����� �⺻ ��ġ.
    private Vector2 originPos = Vector2.zero;

    [SerializeField] List<Rope> ropeList = new List<Rope>();
    [SerializeField] List<ItemBase> itemList = new List<ItemBase>();

    SpriteRenderer leftGround;
    [SerializeField] SpriteRenderer rightGround;

    [SerializeField] float segmentWidth;

    private void Awake()
    {
        originPos = transform.position;
        if (transform.childCount > 0)
            leftGround = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (leftGround != null && rightGround != null)
        {
            segmentWidth = (rightGround.bounds.center.x + rightGround.bounds.size.x / 2)
                           - (leftGround.bounds.center.x - leftGround.bounds.size.x / 2);
        }
        else if (leftGround != null && rightGround == null)
        {
            segmentWidth = leftGround.bounds.size.x;
        }
    }

    // �����Ϳ��� ������ �簢�� (�� ���׸�Ʈ ���� ǥ��)
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {        
        if (transform.childCount > 0)
            leftGround = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (leftGround != null && rightGround != null)
        {
            segmentWidth = (rightGround.bounds.center.x + rightGround.bounds.size.x / 2)
                           - (leftGround.bounds.center.x - leftGround.bounds.size.x / 2);
        }
        else if (leftGround != null && rightGround == null)
        {
            segmentWidth = leftGround.bounds.size.x;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(segmentWidth, 10f, 1f));
    }
#endif

    // ����� ������ ���� ��ġ�� ����
    public void ReturnToOrigin()
    {
        transform.position = originPos;

        int count = ropeList.Count;

        for (int i = 0; i < count; i++)
        {
            ropeList[i].ReturnOrigin();
        }

        count = itemList.Count;

        for (int i = 0; i < count; i++)
        {            
            itemList[i].ReturnOrigin();
        }

        count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == false)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public float GetWidth() { return  segmentWidth; }
}
