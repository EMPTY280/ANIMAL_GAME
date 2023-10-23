using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent : MonoBehaviour
{
    [SerializeField] TutorialMap map;
    [SerializeField] LevelSegmentGroup chapter2;
    [SerializeField] MapScroller scroller;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image textBack;
    [SerializeField] PlayerBase player;
    [SerializeField] RectTransform pauseButton;
    [SerializeField] RectTransform clearItem;
    [SerializeField] TutorialManager manager;

    //CsvReader csvReader = new CsvReader();
    //List<Dialogue> dialogues;

    [SerializeField] int tutorialNum = 0;

    string failedText;

    // Start is called before the first frame update
    void Start()
    {
        //dialogues = csvReader.Read("Tutorial_Dialogue");
        failedText = "±¦Âú¾Æ! ´Ù½Ã ÇÑ¹ø ÇØº¸ÀÚ.";
        text.text = string.Empty;
        textBack.gameObject.SetActive(false);
        StartCoroutine(TutorialProgress());
    }

    IEnumerator TutorialProgress()
    {
        //Dialogue dia = dialogues[tutorialNum];
        EventData eventData = EventManager.Instance.eventCase[tutorialNum];
        float typeDelay = 0.05f;

        yield return new WaitForSeconds(2f);

        textBack.gameObject.SetActive(true);
        foreach (char item in eventData.eventText)
        {
            text.text += item;
            yield return new WaitForSeconds(typeDelay);
        }
        yield return new WaitForSeconds(eventData.time);

        while (true)
        {
            if (map.InProgress() == false)
            {
                if (player.QuestCheck(eventData.condition) == true)
                {
                    tutorialNum++;
                    eventData = EventManager.Instance.eventCase[tutorialNum];
                    text.text = string.Empty;
                    if (textBack.gameObject.activeSelf == false)
                    {
                        textBack.gameObject.SetActive(true);
                    }
                    Tutorial_Spotlight(tutorialNum, true);
                    foreach (char item in eventData.eventText)
                    {
                        text.text += item;                        
                        yield return new WaitForSeconds(typeDelay);
                    }
                    yield return new WaitForSeconds(eventData.time);

                    if (eventData.pieceIndex != 0)
                    {
                        textBack.gameObject.SetActive(false);
                        map.ReserveSegment(eventData.pieceIndex);
                    }
                    Tutorial_Spotlight(tutorialNum, false);
                }
                else
                {
                    text.text = string.Empty;
                    textBack.gameObject.SetActive(true);
                    foreach (char item in failedText)
                    {
                        text.text += item;
                        yield return new WaitForSeconds(typeDelay);
                    }
                    yield return new WaitForSeconds(eventData.time);
                    textBack.gameObject.SetActive(false);
                    map.ReserveSegment(eventData.pieceIndex);
                }
            }
            if (tutorialNum >= 27)
            {
                textBack.gameObject.SetActive(false);
                StartCoroutine(manager.BackGroundChange(1));
                scroller.ChangeMap(1);
                manager.StartRunCheck();
                manager.OnProcessBar(true);
                break;
            }
            yield return null;
        }
        yield return null;
    }

    void Tutorial_Spotlight(int tutoNum, bool state)
    {
        if(state)
        {
            //Rect spot;
            switch (tutoNum)
            {
                case 1:
                    //spot = new Rect(40f, -20f, 120f, 80f);
                    //spot = new Rect(pauseButton.position, pauseButton.sizeDelta);
                    //GameManager.Instance.SetBlackout(true, spot);
                    GameManager.Instance.SetBlackout(true, pauseButton.position,pauseButton.sizeDelta);
                    break;

                case 3:
                    //spot = new Rect(1080f, -20f, 180f, 80f);
                    //spot = new Rect(clearItem.anchoredPosition, clearItem.sizeDelta);
                    //GameManager.Instance.SetBlackout(true, spot);
                    GameManager.Instance.SetBlackout(true, clearItem.position, clearItem.sizeDelta);
                    break;
                default:
                    return;
            }
        }
        else
        {
            GameManager.Instance.SetBlackout(false);
        }
    }
}
