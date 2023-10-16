using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // �����Ϳ����� �⺻ ��ġ.
    private Vector2 originPos = Vector2.zero;

    [SerializeField] List<Rope> ropeList = new List<Rope>();
    [SerializeField] List<ItemBase> itemList = new List<ItemBase>();

    private void Awake()
    {
        originPos = transform.position;
    }

    // �����Ϳ��� ������ �簢�� (�� ���׸�Ʈ ���� ǥ��)
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(22f, 10f, 1f));
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
            itemList[i].gameObject.SetActive(true);
            itemList[i].ReturnOrigin();
        }
    }
}
