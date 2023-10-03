using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    List<MapScroller> maps = new List<MapScroller>();

    //[SerializeField] private float scrollSpeed = 0.2f;

    private void Awake()
    {
        int child = transform.childCount;

        for(int i = 0; i < child; i++)
        {
            maps.Add(transform.GetChild(i).gameObject.GetComponent<MapScroller>());
        }
    }

    public void SetSpeed(float speed)
    {
        // list에 있는 맵 속도 일괄 변경
    }
}
