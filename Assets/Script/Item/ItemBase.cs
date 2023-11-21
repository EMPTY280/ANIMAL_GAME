using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    Vector3 originPos;
    [SerializeField] protected int itemID; // 0 = Ŭ���������, 1 = ��, 2 = ��, 3 = ��, 4 = õ��
    public int ItemID => itemID;
    float speed = 10f;

    void Awake()
    {
        originPos = transform.position;
    }

    private void OnEnable()
    {
        ReturnOrigin();
    }

    public int GetItemID() { return itemID; }

    public void ReturnOrigin()
    {
        transform.position = originPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Magnet"))
        {
            Vector3 pos = collision.transform.position;
            Vector3 dir = pos - transform.position;
            dir = dir.normalized;
            transform.position += dir * Time.deltaTime * speed;
            if(transform.position.x <= pos.x)
            {
                transform.position = new Vector2(pos.x, transform.position.y);
            }
        }
    }
}
