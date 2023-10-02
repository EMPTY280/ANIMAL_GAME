using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // 에디터에서의 기본 위치.
    private Vector2 originPos = Vector2.zero;

    List<Rope> ropeList = new List<Rope>();

    private void Awake()
    {
        originPos = transform.position;

        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            if(transform.GetChild(i).GetComponent<Rope>() != null)
            {
                ropeList.Add(transform.GetChild(i).GetComponent<Rope>());
            }
        }
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

        for(int i =0; i < ropeList.Count; i++)
        {
            ropeList[i].ReturnOrigin();
        }
    }
}
