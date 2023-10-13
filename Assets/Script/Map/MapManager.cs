using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] MapScroller map;
    [SerializeField] BackGroundScroller background;

    [SerializeField] float originSpeed = 0.2f;

    private void Awake()
    {
        map.SetSpeed(originSpeed);
        background.SetSpeed(originSpeed);
    }

    public void SetSpeed(float speed)
    {
        map.SetSpeed(speed);
        background.SetSpeed(speed);
    }

    public void ReturnSpeed()
    {
        map.SetSpeed(originSpeed);
        background.SetSpeed(originSpeed);
    }
}
