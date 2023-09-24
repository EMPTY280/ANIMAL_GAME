using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // �����Ϳ����� �⺻ ��ġ.
    private Vector2 originPos = Vector2.zero;

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
    }
}
