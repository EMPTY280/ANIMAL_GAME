using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_BackGroundScroller : Re_MapScroller
{
    [SerializeField] List<Sprite> images = new List<Sprite>();

    SpriteRenderer first = null;
    SpriteRenderer second = null;

    protected override void Start()
    {
        first = transform.GetChild(0).GetComponent<SpriteRenderer>();
        second = transform.GetChild(1).GetComponent<SpriteRenderer>();

        ChangeMap(mapManager.CurrentMap);

        first.gameObject.SetActive(true);
        second.gameObject.SetActive(true);

        interval = (first.bounds.size.x + second.bounds.size.x) / 2;
    }

    // Update is called once per frame
    protected override void Update()
    {
        offset += scrollSpeed * Time.deltaTime;

        if (offset >= interval)
        {
            offset -= interval;
            SegmentUpdate();
        }

        first.transform.position = new Vector2(0 - offset, 0);
        second.transform.position = new Vector2(interval - offset, 0);
    }

    protected override void SegmentUpdate()
    {
        SpriteRenderer temp = first;
        first = second;
        second = temp;
    }

    public void ChangeMap(int mapNum)
    {
        first.sprite = images[mapNum];
        second.sprite = images[mapNum];
    }
}
