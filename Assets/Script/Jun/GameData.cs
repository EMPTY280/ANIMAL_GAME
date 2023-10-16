
using UnityEngine;

[System.Serializable]
public class EventData
{
    public int index;
    public int pieceIndex;
    public float time;
    public int condition;
    public string eventText;

    //public int Index => index;
}

public static class StaticClass
{
    //static float deltaTime;
    //public static float DeltaTime => (scaleTime*deltaTime);

    //public static float scaleTime;
    public static float TwoPointDistance(Vector3 p1, Vector3 p2)
    {
        float dis = 0;
        dis = Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        return dis;
    }
}

