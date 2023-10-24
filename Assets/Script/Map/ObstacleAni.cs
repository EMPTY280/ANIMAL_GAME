using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAni : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool onmove = false;
    Vector3 dir;
    Vector3 rot;
    float delay = 0f;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = new Vector3(1,2,0); dir.Normalize();
        rot = new Vector3(0,0,-5);
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        onmove = true;
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(onmove)
        {
            delay += Time.deltaTime;
            transform.position += dir * 20f * Time.deltaTime;
            transform.Rotate(rot * 100f * Time.deltaTime);
        }
        
        if(delay >= 1f)
        {
            onmove = false;
            gameObject.SetActive(false);
        }
    }

    public void LoadObstacle(Vector3 pos, Sprite image)
    {
        spriteRenderer.sprite = image;
        transform.position = pos;
    }
}
