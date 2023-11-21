using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_MapSegment : MonoBehaviour
{
    [SerializeField] List<GameObject> variableObjects = new List<GameObject>();

    Vector2 OriginPos;
    Vector2[] ObjectsOriginPos;

    [SerializeField] float segmentLength = 30f;
    [SerializeField] int segmentLevel = 0;
    public float SegmentLength => segmentLength;
    public int SegmentLevel => segmentLevel;


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(SegmentLength, 10f, 0f));
    }

#endif

    protected virtual void Awake()
    {
        int count = variableObjects.Count;
        ObjectsOriginPos = new Vector2[count];

        OriginPos = transform.position;
        for (int i = 0; i < count; i++)
        {
            ObjectsOriginPos[i] = variableObjects[i].transform.localPosition;
        }
    }

    public void ResetSegment()
    {
        int count = variableObjects.Count;

        transform.position = OriginPos;
        for (int i = 0; i < count; i++)
        {
            variableObjects[i].transform.localPosition = ObjectsOriginPos[i];
        }

        count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == false)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        gameObject.SetActive(false);
    }
}
