using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // 에디터에서의 기본 위치.
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

    // 에디터에서 가상의 사각형 (맵 세그먼트 범위 표시)
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

    // 사용이 끝나면 최초 위치로 복귀
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
