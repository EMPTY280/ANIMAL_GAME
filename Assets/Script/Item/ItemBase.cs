using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    Vector3 originPos;
    [SerializeField] protected int itemID; // 0 = Ŭ���������, 1 = ��, 2 = ��, 3 = ��, 4 = õ��

    protected virtual void Awake()
    {
        originPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = originPos;
    }

    public int GetItemID() { return itemID; }
}
