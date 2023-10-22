using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] MapScroller map;
    [SerializeField] BackGroundScroller background;

    [SerializeField] float originSpeed = 0.2f;
    [SerializeField] float farSpeed = 0.125f;
    [SerializeField] float middleSpeed = 0.25f;
    [SerializeField] float closeSpeed = 0.5f;

    private void Awake()
    {
        map.SetSpeed(originSpeed);
        background.SetSpeed(originSpeed * farSpeed, originSpeed * middleSpeed, originSpeed * closeSpeed);
    }

    public void SetSpeed(float times)
    {
        float speed = originSpeed * times;
        map.SetSpeed(speed);
        background.SetSpeed(speed * farSpeed, speed * middleSpeed, speed * closeSpeed);
    }

    public void ReturnSpeed()
    {
        map.SetSpeed(originSpeed);
        background.SetSpeed(originSpeed * farSpeed, originSpeed * middleSpeed, originSpeed * closeSpeed);
    }
}
