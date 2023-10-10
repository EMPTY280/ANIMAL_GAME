using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TutorialEvent : MonoBehaviour
{
    [SerializeField] TutorialMap map;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image textBack;
    [SerializeField] PlayerBase player;

    CsvReader csvReader = new CsvReader();
    List<Dialogue> dialogues;

    [SerializeField] int tutorialNum = 0;

    string failedText;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = csvReader.Read("Tutorial_Dialogue");
        failedText = "±¦Âú¾Æ! ´Ù½Ã ÇÑ¹ø ÇØº¸ÀÚ.";
        text.text = string.Empty;
        textBack.gameObject.SetActive(false);
        StartCoroutine(TutorialProgress());        
    }

    IEnumerator TutorialProgress()
    {
        Dialogue dia = dialogues[tutorialNum];
        float typeDelay = 0.05f;

        yield return new WaitForSeconds(2f);

        textBack.gameObject.SetActive(true);
        foreach (char item in dia.text)
        {
            text.text += item;
            yield return new WaitForSeconds(typeDelay);
        }
        yield return new WaitForSeconds(dia.textTime);

        while (true)
        {
            if (map.InProgress() == false)
            {
                if (player.QuestCheck(dia.questCondition) == true)
                {
                    tutorialNum++;
                    dia = dialogues[tutorialNum];
                    text.text = string.Empty;
                    if (textBack.gameObject.activeSelf == false)
                    {
                        textBack.gameObject.SetActive(true);
                    }
                    foreach (char item in dia.text)
                    {
                        text.text += item;                        
                        yield return new WaitForSeconds(typeDelay);
                    }
                    if (dia.addText != null)
                    {
                        text.text += "\n";
                        foreach (char item in dia.addText)
                        {
                            text.text += item;
                            yield return new WaitForSeconds(typeDelay);
                        }
                    }
                    map.SetDelay(5);
                    yield return new WaitForSeconds(dia.textTime);

                    if (dia.segNum != 0)
                    {
                        textBack.gameObject.SetActive(false);
                        map.ReserveSegment(dia.segNum);
                    }
                    else
                    {
                        map.SetDelay(0);
                    }
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
                    map.SetDelay(5);
                    yield return new WaitForSeconds(dia.textTime);
                    textBack.gameObject.SetActive(false);
                    map.ReserveSegment(dia.segNum);
                }
            }
            yield return null;
        }
    }
}
