using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventManager : Singleton<EventManager>
{
    public Dictionary<int, EventData> eventCase;
    public WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    //public float TwoPointDistance(Vector3 p1, Vector3 p2)
    //{
    //    float dis =  0;
    //    dis = Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    //    return dis;
    //}
}
