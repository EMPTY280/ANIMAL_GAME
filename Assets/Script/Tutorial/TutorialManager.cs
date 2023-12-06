using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] FadePanel fadePanel;
    [SerializeField] BackGroundScroller backGroundScroller;
    [SerializeField] MapScroller mapScroller;
    [SerializeField] MapManager mapManager;
    [SerializeField] Slider processBar;
    [SerializeField] PlayerBase player;
    [SerializeField] ObstacleAniPool aniPool;

    bool distanceCheck = false;
    [SerializeField] float runDistance = 0;
    [SerializeField] float mapDistance = 0;
    [SerializeField] float mapProcess = 0;
    int clearCondition = 5;

    private void Update()
    {
        if (distanceCheck == true)
        {
            runDistance += mapManager.OriginSpeed * Time.deltaTime;
            mapProcess = runDistance / mapDistance;            
            if(mapProcess >= 1)
            {
                mapProcess = 1f;
                distanceCheck = false;
                int clearItem = player.ClearItem;
                GameResult result;
                result.collectedItems = clearItem;
                result.collectGoal = clearCondition;
                result.stageName = "Æ©Åä¸®¾ó";
                result.isClear = clearItem >= clearCondition;
                GameManager.Instance.SaveGameResult(result);
                GameManager.Instance.ChangeScene("Title");
            }
            processBar.value = mapProcess;
        }
    }
    
    public IEnumerator BackGroundChange(int mapNum)
    {
        SpriteRenderer sprite = fadePanel.Sprite;
        Color color = sprite.color;
        WaitForSeconds delay = new WaitForSeconds(0.01f);
        while (color.a < 1.0f)
        {
            color.a += 0.01f;
            yield return delay;
            sprite.color = color;
        }

        yield return new WaitForSeconds(1.5f);
        backGroundScroller.MapChange(mapNum);
        while (color.a > 0f)
        {
            color.a -= 0.01f;
            yield return delay;
            sprite.color = color;
        }
    }

    public void StartRunCheck()
    {
        player.ItemReset();
        runDistance = 0;
        mapDistance = mapScroller.GetMapDistance();
        distanceCheck = true;
    }

    public void OnProcessBar(bool value)
    {
        processBar.gameObject.SetActive(value);
    }

    public void PlayObstacleAni(Vector3 pos, Sprite image)
    {
        aniPool.PlayObstacleAni(pos, image);
    }
}
