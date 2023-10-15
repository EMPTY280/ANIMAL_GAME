using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    Vector3 originPos;
    [SerializeField] protected int itemID; // 0 = 클리어아이템, 1 = 불, 2 = 물, 3 = 숲, 4 = 천사

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
