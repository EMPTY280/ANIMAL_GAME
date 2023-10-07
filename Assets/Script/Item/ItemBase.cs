using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    GameObject effectObject;

    private void Awake()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Item_Water_Effect");
        effectObject = Instantiate(prefab);
    }

    private void OnDisable()
    {
        ItemFunc();
        if(effectObject != null)
        {
            effectObject.SetActive(true);
            effectObject.transform.position = transform.position;
        }
    }

    protected virtual void ItemFunc()
    {

    }
}
