using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAniPool : MonoBehaviour
{
    [SerializeField] GameObject instance; 

    List<GameObject> obstacleAniList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++) 
        {
            GameObject obj = CreateObject();
            obstacleAniList.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }

    GameObject CreateObject()
    {
        GameObject obj = Instantiate(instance, transform);
        return obj;
    }

    public void PlayObstacleAni(Vector3 pos, Sprite image)
    {
        ObstacleAni ani;
        int count = obstacleAniList.Count;
        for (int i = 0; i < count; i++) 
        {
            if (obstacleAniList[i].gameObject.activeSelf == false)
            {
                ani = obstacleAniList[i].gameObject.GetComponent<ObstacleAni>();
                ani.LoadObstacle(pos, image);
                obstacleAniList[i].gameObject.SetActive(true);
                return;
            }
        }
        GameObject obj = CreateObject();
        obstacleAniList.Add(obj);
        ani = obj.gameObject.GetComponent<ObstacleAni>();
        ani.LoadObstacle(pos, image);
        obj.gameObject.SetActive(true);
    }
}
