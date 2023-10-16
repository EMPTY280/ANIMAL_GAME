using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // 에디터에서의 기본 위치.
    private Vector2 originPos = Vector2.zero;

    [SerializeField] List<Rope> ropeList = new List<Rope>();
    [SerializeField] List<ItemBase> itemList = new List<ItemBase>();

    private void Awake()
    {
        originPos = transform.position;
    }

    // 에디터에서 가상의 사각형 (맵 세그먼트 범위 표시)
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(22f, 10f, 1f));
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
            itemList[i].gameObject.SetActive(true);
            itemList[i].ReturnOrigin();
        }
    }
}
